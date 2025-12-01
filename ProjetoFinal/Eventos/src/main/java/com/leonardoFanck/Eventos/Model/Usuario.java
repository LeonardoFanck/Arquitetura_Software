package com.leonardoFanck.Eventos.Model;

import java.util.UUID;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Getter
@Setter
@AllArgsConstructor
@NoArgsConstructor
public class Usuario {
    private UUID id;
    private String nome;
    private String email;
    private String telefone;

}
