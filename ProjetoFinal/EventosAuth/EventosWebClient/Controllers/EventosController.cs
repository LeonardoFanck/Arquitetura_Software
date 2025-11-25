using EventosShared.Model;
using EventosWebClient.Funcoes;
using EventosWebClient.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace EventosWebClient.Controllers;

public class EventosController : Controller
{
	private readonly IHttpClientFactory _httpClientFactory;
	private readonly HttpClient _client;

	public EventosController(IHttpClientFactory httpClientFactory)
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

		var response = await _client.GetAsync("eventos/getAllAvailable");

		if (!response.IsSuccessStatusCode)
		{
			TempData["Error"] = "Erro ao obter a lista de eventos, " + await response.Content.ReadAsStringAsync();
			return View(new List<EventoViewModel>());
		}

		var eventos = await response.Content.ReadFromJsonAsync<List<Evento>>() ?? [];

		response = await _client.GetAsync($"inscricoes/getAllByUser?id={user.Id}");

		if (!response.IsSuccessStatusCode)
		{
			TempData["Error"] = "Erro ao obter a lista de eventos, " + await response.Content.ReadAsStringAsync();
			return View(new List<EventoViewModel>());
		}

		var inscricoes = await response.Content.ReadFromJsonAsync<List<Inscricao>>() ?? [];

		if (!response.IsSuccessStatusCode)
		{
			TempData["Error"] = "Erro ao obter a lista de eventos, " + await response.Content.ReadAsStringAsync();
			return View(new List<EventoViewModel>());
		}

		var meusEventos = eventos.Select(x => new EventoViewModel()
		{
			Evento = x,
			IsInscrito = inscricoes.Any(i => i.EventoId == x.Id)
		}).ToList();

		return View(meusEventos);
	}

	public async Task<IActionResult> Inscrever(Guid id)
	{
		var token = HttpContext.Session.GetString("Token");
		_client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

		var response = await _client.GetAsync($"eventos?id={id}");

		if (!response.IsSuccessStatusCode)
		{
			TempData["Error"] = "Erro ao obter a lista de eventos, " + await response.Content.ReadAsStringAsync();
			response = await _client.GetAsync("eventos/getAll");
			return View("Index", await response.Content.ReadFromJsonAsync<List<Evento>>());
		}

		var evento = await response.Content.ReadFromJsonAsync<Evento>();

		if (evento is null)
		{
			TempData["Error"] = "Evento não encontrado.";
		}
		else
		{
			if(evento.VagasUtilizadas >= evento.Vagas)
			{
				TempData["Error"] = "Não há mais vagas disponíveis para este evento.";
				return RedirectToAction("Index");
			}

			var user = HttpContext.Session.GetUserSession()!;

			var inscricao = new Inscricao()
			{
				DataInscricao = DateTime.Now,
				EventoId = evento.Id,
				Id = Guid.NewGuid(),
				UserId = user.Id
			};

			response = await _client.PostAsJsonAsync("inscricoes", inscricao);

			if (!response.IsSuccessStatusCode)
			{
				TempData["Error"] = "Não foi possivel confirmar a inscrição.";
			}
			else
			{
				TempData["Success"] = $"Inscrição realizada com sucesso no evento '{evento.Titulo}'!";
			}
		}

		return RedirectToAction("Index");


		//response = await _client.GetAsync("eventos/getAll");
		//return View("Index", await response.Content.ReadFromJsonAsync<List<Evento>>());
	}
}
