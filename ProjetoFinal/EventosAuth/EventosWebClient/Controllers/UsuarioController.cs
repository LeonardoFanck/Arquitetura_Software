using EventosShared.Model;
using EventosWebClient.Funcoes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EventosWebClient.Controllers;

public class UsuarioController(IHttpClientFactory httpClientFactory) : Controller
{
	private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

	public IActionResult Login()
	{
		if (HttpContext.Session.GetUserSession() is not null)
			return RedirectToAction("Index", "Home");

		return View(new UserLogin());
	}

	[HttpPost]
	public async Task<IActionResult> Login(UserLogin user)
	{
		if (!ModelState.IsValid)
			return View(user);

		var client = _httpClientFactory.CreateClient("EventosAuth");

		var response = await client.PostAsJsonAsync("auth/login", user);

		if (!response.IsSuccessStatusCode)
		{
			ModelState.AddModelError("", await response.Content.ReadAsStringAsync());
			return View(user);
		}

		var loginResp = await response.Content.ReadAsStringAsync();

		if (string.IsNullOrEmpty(loginResp))
		{
			ModelState.AddModelError("", "Usuário ou senha inválidos");
			return View(user);
		}

		// --------------- USANDO SESSION -----------------
		HttpContext.Session.SetString("Token", loginResp);
		var codigoUser = new JwtSecurityTokenHandler().ReadJwtToken(loginResp).Claims.First(x => x.Type == "userID").Value;

		client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResp);

		response = await client.GetAsync($"users?id={codigoUser}");

		if (!response.IsSuccessStatusCode)
		{
			ModelState.AddModelError("", "Não foi possível obter os dados do usuário.");
			return View(user);
		}

		var userResponse = await response.Content.ReadFromJsonAsync<User>();

		if (userResponse is null)
		{
			ModelState.AddModelError("", "Não foi possível obter os dados do usuário.");
			return View(user);
		}

		HttpContext.Session.SetUserSession(userResponse);

		// OK
		return RedirectToAction("Index", "Home");
	}

	public IActionResult Logout()
	{
		HttpContext.Session.RemoveUserSession();
		HttpContext.Session.Remove("Token");
		return RedirectToAction("Index", "Home");
	}

	public IActionResult Perfil()
	{
		var user = HttpContext.Session.GetUserSession();
		if (user is null)
			return RedirectToAction("Login", "Usuario");

		user.Senha = "";
		return View(user);
	}

	[HttpPost]
	public async Task<IActionResult> Edit(User user)
	{
		ModelState["Senha"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;

		if (!ModelState.IsValid)
			return View("Perfil",user);

		var userSession = HttpContext.Session.GetUserSession();

		if(string.IsNullOrEmpty(user.Senha))
		{
			if (userSession is not null)
				user.Senha = userSession.Senha;
		}

		user.Id = userSession!.Id;

		var client = _httpClientFactory.CreateClient("EventosAuth");

		client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
		var response = await client.PutAsJsonAsync("users", user);

		if (!response.IsSuccessStatusCode)
		{
			ModelState.AddModelError("", await response.Content.ReadAsStringAsync());
			user.Senha = "";
			return View("Perfil", user);
		}

		TempData["Success"] = "Perfil atualizado com sucesso!";

		user.Senha = "";
		return View("Perfil",user);


		
	}
}
