using EventosShared.Model;
using EventosWebClient.Funcoes;
using Microsoft.AspNetCore.Mvc;

namespace EventosWebClient.Controllers;

public class CertificadoController : Controller
{
	private readonly IHttpClientFactory _httpClientFactory;
	private readonly HttpClient _clientCertificado;
	private readonly HttpClient _clientEvento;

	public CertificadoController(IHttpClientFactory httpClientFactory)
	{
		_httpClientFactory = httpClientFactory;
		_clientCertificado = _httpClientFactory.CreateClient("EventosCertificado");
		_clientEvento = _httpClientFactory.CreateClient("EventosEvento");
	}

	public IActionResult Index()
	{
		return View();
	}

	public async Task<IActionResult> Gerar(Guid idInscricao)
	{
		var token = HttpContext.Session.GetString("Token");
		_clientCertificado.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
		_clientEvento.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

		var user = HttpContext.Session.GetUserSession();

		if (user is null)
			return RedirectToAction("Login", "Usuario");

		var response = await _clientEvento.GetAsync($"inscricoes?id={idInscricao}");


		if (!response.IsSuccessStatusCode)
		{
			TempData["Error"] = "Erro ao obter a inscrição, " + await response.Content.ReadAsStringAsync();
			return RedirectToAction("Index", "Inscricoes");
		}

		var inscricao = await response.Content.ReadFromJsonAsync<Inscricao>();

		if (inscricao is null)
		{
			TempData["Error"] = "Erro ao obter a inscrição";
			return RedirectToAction("Index", "Inscricoes");
		}

		var certificado = new Certificado()
		{
			Id = Guid.NewGuid(),
			UserId = user.Id,
			EventoId = inscricao.EventoId,
			DataEmissao = DateTime.UtcNow,
			CodigoAutenticacao = Guid.NewGuid().ToString().Replace("-", "")[..10].ToUpper() + Guid.NewGuid().ToString().Replace("-", "")[..10].ToUpper() + Guid.NewGuid().ToString().Replace("-", "")[..10].ToUpper() + Guid.NewGuid().ToString().Replace("-", "")[..10].ToUpper() + Guid.NewGuid().ToString().Replace("-", "")[..10].ToUpper()
		};

		response = await _clientCertificado.PostAsJsonAsync($"certificados", certificado);

		if (!response.IsSuccessStatusCode)
		{
			TempData["Error"] = "Erro ao criar certificado, " + await response.Content.ReadAsStringAsync();
			return RedirectToAction("Index", "Inscricoes");
		}

		TempData["Success"] = "Certificado criado com sucesso!";

		//var eventos = await response.Content.ReadFromJsonAsync<List<Evento>>() ?? [];

		//response = await _clientCertificado.GetAsync($"inscricoes/getAllByUser?id={user.Id}");

		//if (!response.IsSuccessStatusCode)
		//{
		//	TempData["Error"] = "Erro ao obter a lista de eventos, " + await response.Content.ReadAsStringAsync();
		//	return View(new List<EventoViewModel>());
		//}

		return RedirectToAction("Index", "Inscricoes");
	}

	public async Task<IActionResult> Abrir(Guid id)
	{
		var token = HttpContext.Session.GetString("Token");
		_clientCertificado.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

		var response = await _clientCertificado.GetAsync($"certificados/{id}");

		if (!response.IsSuccessStatusCode)
			return BadRequest("Erro ao obter o certificado");

		var certificado = await response.Content.ReadFromJsonAsync<Certificado>();

		if (certificado == null || certificado.PdfArquivo == null)
			return BadRequest("PDF não encontrado");

		return File(certificado.PdfArquivo, "application/pdf", $"certificado-{id}.pdf");
	}

	public IActionResult Validar()
	{
		return View();
	}

	[HttpPost]
	public async Task<IActionResult> Validar(string codigo)
	{
		//var token = HttpContext.Session.GetString("Token");
		//_clientCertificado.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

		var response = await _clientCertificado.GetAsync($"certificados/getCertificadoByCodigo/{codigo}");

		if (!response.IsSuccessStatusCode)
		{
			TempData["Error"] = "Certificado inválido!";
			return View("Validar", codigo);
		}

		var certificado = await response.Content.ReadFromJsonAsync<Certificado>();

		if (certificado == null)
		{
			TempData["Error"] = "Certificado inválido!";
			return View("Validar", codigo);
		}

		TempData["Success"] = "Certificado válido!";

		return View("Validar", codigo);
	}
}
