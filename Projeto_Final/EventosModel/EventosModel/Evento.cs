namespace EventosModel;

public class Evento
{
    public Guid Id { get; set; }


    // Metadados
    public string Titulo { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public DateTimeOffset DataInicio { get; set; }
    public DateTimeOffset? DataFim { get; set; }
    
    // Capacidade e estado
    public int? Capacidade { get; set; }
    
    // Auditoria
    public DateTimeOffset CriadoEm { get; set; } = DateTimeOffset.UtcNow;
}
