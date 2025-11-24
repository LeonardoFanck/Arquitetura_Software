namespace EventosShared.Model;

public class CheckIn
{
	public Guid Id { get; set; }
	public Guid UserId { get; set; }
	public Guid EventoId { get; set; }

	public DateTime DataCheckIn { get; set; }
}
