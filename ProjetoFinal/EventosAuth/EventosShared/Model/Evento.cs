namespace EventosShared.Model;

public class Evento
{
	public Guid Id { get; set; }
	public string Titulo { get; set; } = "";
	public string Descricao { get; set; } = "";
	public DateTime DataInicio { get; set; }
	public DateTime DataFim { get; set; }
	public int Vagas { get; set; }
	public int VagasUtilizadas { get; set; }
}
