package com.leonardoFanck.Eventos.Model;

import java.util.Date;
import java.util.UUID;

import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class CheckIn {
    private UUID Id;
    private UUID UserId;
    private UUID EventoId;
    private Date DataCheckIn;

}
