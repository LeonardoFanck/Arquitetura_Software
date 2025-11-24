package Config;

import jakarta.servlet.FilterChain;
import jakarta.servlet.ServletException;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;
import org.springframework.http.MediaType;
import org.springframework.security.authentication.UsernamePasswordAuthenticationToken;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.web.filter.OncePerRequestFilter;
import io.jsonwebtoken.Claims;

import java.io.IOException;
import java.util.List;

public class JwtAuthFilter extends OncePerRequestFilter {

    private final JwtUtil jwtUtil;

    // Lista simples de rotas públicas
    private final List<String> publicPaths = List.of(
            "/public/",
            "/public/",
            "/actuator/"
    );

    public JwtAuthFilter(JwtUtil jwtUtil) {
        this.jwtUtil = jwtUtil;
    }

    private boolean isPublic(HttpServletRequest request) {
        String path = request.getServletPath();
        return publicPaths.stream().anyMatch(path::startsWith);
    }

    @Override
    protected void doFilterInternal(
            HttpServletRequest request,
            HttpServletResponse response,
            FilterChain filterChain
    ) throws ServletException, IOException {

        // Se for rota pública -> permite
        if (isPublic(request)) {
            filterChain.doFilter(request, response);
            return;
        }

        String header = request.getHeader("Authorization");

        if (header == null || !header.startsWith("Bearer ")) {
            unauthorized(response, "Token ausente");
            return;
        }

        String token = header.substring(7);

        try {
            Claims claims = jwtUtil.validateAndGetClaims(token);
            String username = claims.getSubject();

            var auth = new UsernamePasswordAuthenticationToken(
                    username,
                    null,
                    List.of() // Authorities vazias
            );

            SecurityContextHolder.getContext().setAuthentication(auth);
            filterChain.doFilter(request, response);

        } catch (Exception ex) {
            unauthorized(response, "Token inválido");
        }
    }

    private void unauthorized(HttpServletResponse response, String msg) throws IOException {
        response.setStatus(401);
        response.setContentType(MediaType.APPLICATION_JSON_VALUE);
        response.getWriter().write("{\"error\":\"" + msg + "\"}");
    }
}