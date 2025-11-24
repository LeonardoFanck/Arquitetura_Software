package com.leonardoFanck.Eventos.Controller;

import com.leonardoFanck.Eventos.Model.Certificado;
import com.leonardoFanck.Eventos.Repository.CertificadoRepository;
import org.hibernate.annotations.NotFound;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;

import java.util.List;
import java.util.UUID;

@RestController
@RequestMapping("/certificados")
public class CertificadosController
{
    private final CertificadoRepository _context;

    public CertificadosController(CertificadoRepository context){
        _context = context;
    }

    @GetMapping
    public List<Certificado> GetAll(){
        return _context.findAll();
    }

    @GetMapping("/{id}")
    public Certificado GetById(@PathVariable UUID id){
        return _context.findById(id).orElseThrow(() -> new ResponseStatusException(HttpStatus.NOT_FOUND, "Certificado não encontrado"));
    }


    @PostMapping
    public Certificado create(@RequestBody Certificado user) {
        return _context.save(user);
    }

    @PutMapping("/{id}")
    public Certificado update(@PathVariable UUID id, @RequestBody Certificado certificado) {
        Certificado existing = _context.findById(id)
                .orElseThrow(() -> new ResponseStatusException(HttpStatus.NOT_FOUND, "Certificado não encontrado"));

        certificado.setId(existing.getId()); // garante id correto
        return _context.save(certificado);
    }

    @DeleteMapping("/{id}")
    public void delete(@PathVariable UUID id) {

        Certificado certificado = _context.findById(id).orElseThrow(() -> new ResponseStatusException(HttpStatus.NOT_FOUND, "Certificado não encontrado"));

        _context.delete(certificado);
    }
}
