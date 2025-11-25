using EventosShared.Model;

namespace EventosWebClient.ViewModel;

public class EventoViewModel
{
	public Evento Evento { get; set; } = new();
	public bool IsInscrito { get; set; } = false;
}
