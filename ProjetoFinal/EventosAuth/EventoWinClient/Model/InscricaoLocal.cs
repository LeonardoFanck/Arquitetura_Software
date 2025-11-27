using EventosShared.Model;

namespace EventoWinClient.Model;

public class InscricaoLocal : Inscricao
{
	public bool Sincronizado { get; set; } = true;
}
