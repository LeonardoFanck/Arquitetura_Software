package com.leonardoFanck.Eventos.Controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import com.leonardoFanck.Eventos.Model.EmailRequest;
import com.leonardoFanck.Eventos.Service.EmailService;

@RestController
@RequestMapping("/email")
public class EmailController {

    @Autowired
    private EmailService emailService;

    @PostMapping("/enviar")
    public String enviarEmail(@RequestBody EmailRequest request) {
        emailService.enviarEmail(request.getDestinatario(), request.getMensagem());
        return "Email enviado com sucesso para: " + request.getDestinatario();
    }
}