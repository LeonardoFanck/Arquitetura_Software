package com.leonardoFanck.Eventos.Model;

import java.util.Date;
import java.util.UUID;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Getter
@Setter
@AllArgsConstructor
@NoArgsConstructor
public class Evento {
    private UUID id;
    private String titulo;
    private String descricao;
    private Date dataInicio;
    private Date dataFim;
    private int vagas;

}
