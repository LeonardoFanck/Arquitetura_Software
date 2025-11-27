using EventosShared.Model;

namespace EventoWinClient.Model;

public class CheckInLocal : CheckIn
{
	public bool Sincronizado { get; set; } = true;
}
