package com.leonardoFanck.Eventos.Controller;

import com.itextpdf.kernel.pdf.PdfDocument;
import com.itextpdf.kernel.pdf.PdfWriter;
import com.itextpdf.layout.Document;
import com.itextpdf.layout.element.Paragraph;
import com.itextpdf.layout.properties.TextAlignment;
import com.leonardoFanck.Eventos.Model.Certificado;
import com.leonardoFanck.Eventos.Model.CheckIn;
import com.leonardoFanck.Eventos.Model.Evento;
import com.leonardoFanck.Eventos.Model.Usuario;
import com.leonardoFanck.Eventos.Repository.CertificadoRepository;
import org.apache.coyote.Response;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.*;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.client.RestTemplate;
import org.springframework.web.reactive.function.client.WebClient;
import org.springframework.web.server.ResponseStatusException;

import java.io.ByteArrayOutputStream;
import java.time.LocalDateTime;
import java.util.List;
import java.util.UUID;

@RestController
@RequestMapping("/certificados")
public class CertificadosController {

    @Autowired
    private final WebClient webClient;
    private final CertificadoRepository _context;

    private final AuthController auth;

    public CertificadosController(CertificadoRepository context, AuthController auth, WebClient webClient){
        _context = context;
        this.auth = auth;
        this.webClient = webClient;
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

    @GetMapping("/GetAllByUserId/{id}")
    public List<Certificado> GetAllByUserId(@PathVariable UUID id, @RequestHeader("Authorization") String authHeader){
        validar(authHeader);
        return _context.findAllByUserId(id);
    }

    @GetMapping("/{id}")
    public Certificado GetById(@PathVariable UUID id, @RequestHeader("Authorization") String authHeader){
        validar(authHeader);

        return _context.findById(id)
                .orElseThrow(() -> new ResponseStatusException(HttpStatus.NOT_FOUND, "Certificado não encontrado"));
    }

    @GetMapping("/getCertificadoByCodigo/{id}")
    public Certificado getCertificadoByCodigo(@PathVariable String id){

        Certificado certificado = _context.findByCodigoAutenticacao(id);

        if(certificado == null)
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Certificado não encontrado");

        return certificado;
    }

    @PostMapping
    public Certificado create(@RequestBody Certificado certificado, 
                              @RequestHeader("Authorization") String authHeader) {
        validar(authHeader);

        certificado.setId(null);
        //certificado.setDataEmissao(LocalDateTime.now());
        //certificado.setCodigoAutenticacao(UUID.randomUUID().toString().replace("-","").toUpperCase());

        certificado.setPdfArquivo(gerarPdf(certificado, authHeader));

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

    private byte[] gerarPdf(Certificado cert, String token) {

        Usuario usuario = webClient.get()
                .uri("https://localhost:7015/users?id=" + cert.getUserId())
                .header("Authorization", token)
                .retrieve()
                .bodyToMono(Usuario.class)
                .block();

        // -------- 2) Buscar evento --------
        Evento evento = webClient.get()
                .uri("https://localhost:7153/eventos?id=" + cert.getEventoId())
                .header("Authorization", token)
                .retrieve()
                .bodyToMono(Evento.class)
                .block();

//        CheckIn checkIn = webClient.get()
//                .uri("http://localhost:5002/checkIn?id=" + cert.getEventoId())  // ajuste URL da sua API
//                .header("Authorization", "Bearer " + token)
//                .retrieve()
//                .bodyToMono(Evento.class)
//                .block();


        // -------- 3) Gerar PDF --------
        ByteArrayOutputStream baos = new ByteArrayOutputStream();

        PdfWriter writer = new PdfWriter(baos);
        PdfDocument pdf = new PdfDocument(writer);
        Document doc = new Document(pdf);

        doc.add(new Paragraph("CERTIFICADO DE PARTICIPAÇÃO")
                .setFontSize(24)
                .setTextAlignment(TextAlignment.CENTER));

        doc.add(new Paragraph("\nCertificamos que:")
                .setFontSize(14));

        doc.add(new Paragraph(usuario.getNome())
                .setFontSize(18)

                .setTextAlignment(TextAlignment.CENTER));

        doc.add(new Paragraph("\nEsteve presente no evento:")
                .setFontSize(14));

        doc.add(new Paragraph(evento.getTitulo())
                .setFontSize(16)

                .setTextAlignment(TextAlignment.CENTER));

        doc.add(new Paragraph("\nDescrição do Evento:\n" + evento.getDescricao())
                .setFontSize(12));

        doc.add(new Paragraph("\nData do Evento: "
                + evento.getDataInicio() + " até " + evento.getDataFim())
                .setFontSize(12));

        doc.add(new Paragraph("\nCódigo de Autenticação: " + cert.getCodigoAutenticacao())
                .setFontSize(12));

        doc.add(new Paragraph("\nData de Emissão: " + cert.getDataEmissao())
                .setFontSize(12));

        doc.add(new Paragraph("\n\nValide seu certificado em: https://meusistema.com/validar/" + cert.getCodigoAutenticacao())
                .setFontSize(12));

        doc.close();

        return baos.toByteArray();
    }

}
