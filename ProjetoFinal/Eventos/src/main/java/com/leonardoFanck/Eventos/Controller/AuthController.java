package com.leonardoFanck.Eventos.Controller;

import java.util.Date;
import java.util.Map;
import java.util.UUID;

import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.server.ResponseStatusException;

import com.auth0.jwt.JWT;
import com.auth0.jwt.algorithms.Algorithm;
import com.auth0.jwt.interfaces.DecodedJWT;

@RestController
@RequestMapping("/auth")
public class AuthController {

    private final String SECRET = "supersecretkeythatshouldbeverylongandcomplex"; // troque isso

    @PostMapping("/login")
    public Map<String, String> login(@RequestBody Map<String, String> login) {
        String usuario = login.get("usuario");
        String senha = login.get("senha");

        // valida usuário e senha de forma simples
        if (!usuario.equals("admin") || !senha.equals("123")) {
            throw new ResponseStatusException(HttpStatus.UNAUTHORIZED, "Usuário incorreto");
        }

        // cria token com JWT
        String token = JWT.create()
            .withSubject(usuario)
            .withIssuedAt(new Date())
            .withExpiresAt(new Date(System.currentTimeMillis() + 1000 * 60 * 60)) // 1 hora
            .sign(Algorithm.HMAC256(SECRET));

        return Map.of("token", token);
    }

    public String validarToken(String token) {
        try {
            DecodedJWT decoded = JWT.require(Algorithm.HMAC256(SECRET))
                .build()
                .verify(token);

            return decoded.getSubject(); // usuário autenticado

        } catch (Exception e) {
            throw new ResponseStatusException(HttpStatus.UNAUTHORIZED, "Token inválido");
        }
    }

    public UUID getUserIdFromToken(String token) {
        try {
            if (token.startsWith("Bearer "))
                token = token.substring(7);

            DecodedJWT decoded = JWT.require(Algorithm.HMAC256(SECRET))
                    .build()
                    .verify(token);

            // Pega a claim "userID" criada no C#
            String userIdStr = decoded.getClaim("userID").asString();

            if (userIdStr == null || userIdStr.isEmpty()) {
                throw new ResponseStatusException(HttpStatus.UNAUTHORIZED, "Token sem userID");
            }

            return UUID.fromString(userIdStr);

        } catch (Exception e) {
            throw new ResponseStatusException(HttpStatus.UNAUTHORIZED, "Token inválido ou userID ausente");
        }
    }


}
