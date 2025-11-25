using Microsoft.AspNetCore.Mvc;

namespace EventosWebClient.Controllers;

public class CertificadoController : Controller
{
	public IActionResult Index()
	{
		return View();
	}

	public async Task<IActionResult> Gerar(Guid idInscricao)
	{
		return Ok();
	}
}
