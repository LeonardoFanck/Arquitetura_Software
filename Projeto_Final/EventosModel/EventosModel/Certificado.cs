using System;

namespace EventosModel;

public class Certificado
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid EventoId { get; set; }
    public Guid ParticipanteId { get; set; }

    // Código único impresso no certificado
    public string CodigoAutenticacao { get; set; } = GerarCodigo();

    public DateTimeOffset EmitidoEm { get; set; } = DateTimeOffset.UtcNow;
    public bool Gerado { get; set; } = true;

    public string ObterUrlValidacao(string baseUrl)
    {
        if (string.IsNullOrWhiteSpace(baseUrl))
            baseUrl = "https://validacao.certificados.example.com";
        return $"{baseUrl.TrimEnd('/')}/validar/{CodigoAutenticacao}";
    }

    public static string GerarCodigo()
    {
        return Guid.NewGuid().ToString("N").ToUpperInvariant().Substring(0, 16);
    }
}