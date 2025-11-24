package Config;

import io.jsonwebtoken.Claims;
import io.jsonwebtoken.Jwts;
import io.jsonwebtoken.security.Keys;
import java.security.Key;

public class JwtUtil {

    // use a mesma chave que você usa no C# (mantenha segura!)
    private final Key key;

    public JwtUtil(String secret) {
        this.key = Keys.hmacShaKeyFor(secret.getBytes());
    }

    public Claims validateAndGetClaims(String token) {
        return Jwts.parserBuilder()
                .setSigningKey(key)
                .build()
                .parseClaimsJws(token)
                .getBody();
    }

    public boolean isValid(String token) {
        try {
            validateAndGetClaims(token);
            return true;
        } catch (Exception ex) {
            return false;
        }
    }
}
