using EventosShared.Model;

namespace EventoWinClient.Model;

public class EventoLocal : Evento
{
	public bool Sincronizado { get; set; } = true;
}
