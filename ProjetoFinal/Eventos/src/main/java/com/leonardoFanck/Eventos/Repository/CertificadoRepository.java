package com.leonardoFanck.Eventos.Repository;

import com.leonardoFanck.Eventos.Model.Certificado;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.UUID;

public interface CertificadoRepository extends JpaRepository<Certificado, UUID>{

    Certificado findByUserIdAndEventoId(UUID userId, UUID eventoId);
}
