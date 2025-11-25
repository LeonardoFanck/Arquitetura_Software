package com.leonardoFanck.Eventos.Controller;

import com.leonardoFanck.Eventos.Model.Certificado;
import com.leonardoFanck.Eventos.Repository.CertificadoRepository;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;

import java.util.List;
import java.util.UUID;

@RestController
@RequestMapping("/certificados")
public class CertificadosController {

    private final CertificadoRepository _context;

    private final AuthController auth;

    public CertificadosController(CertificadoRepository context, AuthController auth){
        _context = context;
        this.auth = auth;
    }

    private void validar(String authHeader) {
        if (authHeader == null || !authHeader.startsWith("Bearer ")) {
            throw new ResponseStatusException(HttpStatus.UNAUTHORIZED, "Token não informado");
        }
    
        String token = authHeader.replace("Bearer ", "");
        auth.validarToken(token); // dispara erro se inválido
    }
    
    
    
    @GetMapping
    public List<Certificado> GetAll(@RequestHeader("Authorization") String authHeader){
        validar(authHeader);
        return _context.findAll();
    }


    @GetMapping("/{id}")
    public Certificado GetById(@PathVariable UUID id, @RequestHeader("Authorization") String authHeader){
        validar(authHeader);

        return _context.findById(id)
                .orElseThrow(() -> new ResponseStatusException(HttpStatus.NOT_FOUND, "Certificado não encontrado"));
    }


    @PostMapping
    public Certificado create(@RequestBody Certificado certificado, 
                              @RequestHeader("Authorization") String authHeader) {
        validar(authHeader);
        return _context.save(certificado);
    }
    

    @PutMapping("/{id}")
    public Certificado update(@PathVariable UUID id,
                              @RequestBody Certificado certificado,
                              @RequestHeader("Authorization") String authHeader) {
    
        validar(authHeader);
    
        Certificado existing = _context.findById(id)
                .orElseThrow(() -> new ResponseStatusException(HttpStatus.NOT_FOUND, "Certificado não encontrado"));
    
        certificado.setId(existing.getId());
        return _context.save(certificado);
    }
    

    @DeleteMapping("/{id}")
    public void delete(@PathVariable UUID id, @RequestHeader("Authorization") String authHeader) {
    
        validar(authHeader);
    
        Certificado certificado = _context.findById(id)
                .orElseThrow(() -> new ResponseStatusException(HttpStatus.NOT_FOUND, "Certificado não encontrado"));
    
        _context.delete(certificado);
    }
    
}
