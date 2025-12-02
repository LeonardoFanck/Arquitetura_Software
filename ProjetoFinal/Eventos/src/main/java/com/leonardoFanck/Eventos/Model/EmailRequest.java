package com.leonardoFanck.Eventos.Model;

import lombok.*;

@AllArgsConstructor
@NoArgsConstructor
@Data
public class EmailRequest {
    private String destinatario;
    private String mensagem;

    public String getDestinatario() {
        return destinatario;
    }

    public void setDestinatario(String destinatario) {
        this.destinatario = destinatario;
    }

    public String getMensagem() {
        return mensagem;
    }

    public void setMensagem(String mensagem) {
        this.mensagem = mensagem;
    }
}
