using System;

namespace EventosModel;

public class Participante
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Documento { get; set; } = string.Empty; // CPF/CNPJ ou outro
    public string Endereco { get; set; } = string.Empty;

    public DateTimeOffset CriadoEm { get; set; } = DateTimeOffset.UtcNow;
}