using EventosShared.Model;

namespace EventosWebClient.ViewModel;

public class InscricoesViewModel
{
	public Evento Evento { get; set; } = new();
	public Inscricao Inscricao { get; set; } = new();
}
