package com.leonardoFanck.Eventos.Model;

import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

import java.time.LocalDateTime;
import java.util.UUID;

@Getter
@Setter
@Entity
@AllArgsConstructor
@NoArgsConstructor
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

}
