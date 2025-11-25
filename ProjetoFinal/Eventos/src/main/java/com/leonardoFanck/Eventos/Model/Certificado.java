package com.leonardoFanck.Eventos.Model;

import jakarta.persistence.*;

import java.time.LocalDateTime;
import java.util.UUID;

@Entity
@Table(name = "certificado")
public class Certificado {

    @Id
    @GeneratedValue
    private UUID id;

    private UUID userId;
    private UUID eventoId;

    private LocalDateTime DataEmissao;
    private String CodigoAutenticacao;

    @Lob
    private byte[] pdfArquivo;

    public Certificado(){

    }

    public void setId(UUID id) {
        this.id = id;
    }

    public UUID getId(){
        return id;
    }

    public UUID getUserId() {
        return userId;
    }

    public void setUserId(UUID userId) {
        this.userId = userId;
    }

    public UUID getEventoId() {
        return eventoId;
    }

    public void setEventoId(UUID eventoId) {
        this.eventoId = eventoId;
    }

    public LocalDateTime getDataEmissao() {
        return DataEmissao;
    }

    public void setDataEmissao(LocalDateTime dataEmissao) {
        DataEmissao = dataEmissao;
    }

    public String getCodigoAutenticacao() {
        return CodigoAutenticacao;
    }

    public void setCodigoAutenticacao(String codigoAutenticacao) {
        CodigoAutenticacao = codigoAutenticacao;
    }

    public byte[] getPdfArquivo() {
        return pdfArquivo;
    }

    public void setPdfArquivo(byte[] pdfArquivo) {
        this.pdfArquivo = pdfArquivo;
    }
}
