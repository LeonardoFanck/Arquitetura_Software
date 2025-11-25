namespace EventosShared.Model;

public class Inscricao
{
	public Guid Id { get; set; }
	public Guid UserId { get; set; }
	public Guid EventoId { get; set; }

	public DateTime DataInscricao { get; set; }

}
