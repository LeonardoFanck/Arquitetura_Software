using EventosShared.Model;

namespace EventoWinClient.Model;

public class UserLocal : User
{
	public bool Sincronizado { get; set; } = true;
}
