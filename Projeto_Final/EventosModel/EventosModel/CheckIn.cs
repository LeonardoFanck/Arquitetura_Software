namespace EventosModel;

public class CheckIn
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid EventoId { get; set; }
    public Guid ParticipanteId { get; set; }
    public Guid? InscricaoId { get; set; }

    public DateTimeOffset HoraCheckIn { get; set; } = DateTimeOffset.UtcNow;
}