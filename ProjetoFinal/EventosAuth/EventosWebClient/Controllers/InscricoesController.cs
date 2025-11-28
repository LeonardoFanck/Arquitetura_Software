using EventosShared.Model;
using EventosWebClient.Funcoes;
using EventosWebClient.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EventosWebClient.Controllers;

public class InscricoesController : Controller
{
	private readonly IHttpClientFactory _httpClientFactory;
	private readonly HttpClient _client;
	private readonly HttpClient _clientCertificado;

	public InscricoesController(IHttpClientFactory httpClientFactory)
	{
		_httpClientFactory = httpClientFactory;
		_client = _httpClientFactory.CreateClient("EventosEvento");
		_clientCertificado = _httpClientFactory.CreateClient("EventosCertificado");
	}

	public async Task<IActionResult> Index()
	{
		var token = HttpContext.Session.GetString("Token");
		_client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
		_clientCertificado.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

		var user = HttpContext.Session.GetUserSession();

		if (user is null)
			return RedirectToAction("Login", "Usuario");

		var response = await _client.GetAsync("eventos/getAll");

		if (!response.IsSuccessStatusCode)
		{
			TempData["Error"] = "Erro ao obter a lista de inscrições, " + await response.Content.ReadAsStringAsync();
			return View(new List<InscricoesViewModel>());
		}

		var eventos = await response.Content.ReadFromJsonAsync<List<Evento>>() ?? [];

		response = await _client.GetAsync($"inscricoes/getAllByUser?id={user.Id}");

		if (!response.IsSuccessStatusCode)
		{
			TempData["Error"] = "Erro ao obter a lista de inscrições, " + await response.Content.ReadAsStringAsync();
			return View(new List<InscricoesViewModel>());
		}

		var inscricoes = await response.Content.ReadFromJsonAsync<List<Inscricao>>() ?? [];

		response = await _client.GetAsync($"checkIn/GetAllByUserId?id={user.Id}");

		if (!response.IsSuccessStatusCode)
		{
			TempData["Error"] = "Erro ao obter a lista de checkIns, " + await response.Content.ReadAsStringAsync();
			return View(new List<InscricoesViewModel>());
		}

		var checkIns = await response.Content.ReadFromJsonAsync<List<CheckIn>>() ?? [];

		response = await _clientCertificado.GetAsync($"certificados/GetAllByUserId/{user.Id}");

		if (!response.IsSuccessStatusCode)
		{
			TempData["Error"] = "Erro ao obter a lista de certificados, " + await response.Content.ReadAsStringAsync();
			return View(new List<InscricoesViewModel>());
		}

		var certificados = await response.Content.ReadFromJsonAsync<List<Certificado>>() ?? [];

		var minhasInscricoes = (from inscricao in inscricoes
								join evento in eventos
									on inscricao.EventoId equals evento.Id into gj
								from evento in gj.DefaultIfEmpty()   // permite null = LEFT JOIN
								select new InscricoesViewModel
								{
									Evento = evento ?? new Evento(),   // evita null reference no front
									Inscricao = inscricao,
									CheckIn = checkIns.FirstOrDefault(c => c.EventoId == evento.Id),
									Certificado = certificados.FirstOrDefault(x => x.EventoId == evento.Id)
								}).ToList();

		return View(minhasInscricoes);
	}

	public async Task<IActionResult> Cancelar(Guid id)
	{
		var token = HttpContext.Session.GetString("Token");
		_client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

		//var user = HttpContext.Session.GetUserSession();


		var response = await _client.DeleteAsync($"inscricoes?id={id}");

		if (!response.IsSuccessStatusCode)
			TempData["Error"] = "Erro ao tentar deletar a inscrição, " + await response.Content.ReadAsStringAsync();

		return RedirectToAction("Index");
	}
}
