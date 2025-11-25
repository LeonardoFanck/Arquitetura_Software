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

	public InscricoesController(IHttpClientFactory httpClientFactory)
	{
		_httpClientFactory = httpClientFactory;
		_client = _httpClientFactory.CreateClient("EventosEvento");
	}

	public async Task<IActionResult> Index()
	{
		var token = HttpContext.Session.GetString("Token");
		_client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

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

		//if (!response.IsSuccessStatusCode)
		//{
		//	TempData["Error"] = "Erro ao obter a lista de inscrições, " + await response.Content.ReadAsStringAsync();
		//	return View(new List<InscricoesViewModel>());
		//}

		var minhasInscricoes = (from inscricao in inscricoes
							join evento in eventos on inscricao.EventoId equals evento.Id
							select new InscricoesViewModel
							{
								Evento = evento,
								Inscricao = inscricao
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
