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

    @Column(name = "user_id")
    private UUID userId;
    private UUID eventoId;

    @Column(name = "data_emissao")
    private LocalDateTime dataEmissao;

    @Column(name = "codigo_autenticacao")
    private String codigoAutenticacao;

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
        return dataEmissao;
    }

    public void setDataEmissao(LocalDateTime dataEmissao) {
        this.dataEmissao = dataEmissao;
    }

    public String getCodigoAutenticacao() {
        return codigoAutenticacao;
    }

    public void setCodigoAutenticacao(String codigoAutenticacao) {
        this.codigoAutenticacao = codigoAutenticacao;
    }

    public byte[] getPdfArquivo() {
        return pdfArquivo;
    }

    public void setPdfArquivo(byte[] pdfArquivo) {
        this.pdfArquivo = pdfArquivo;
    }
}
