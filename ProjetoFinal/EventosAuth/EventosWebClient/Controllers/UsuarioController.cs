using EventosShared.Model;
using Microsoft.AspNetCore.Mvc;
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

        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", loginResp);

        var emailEncoded = Uri.EscapeDataString(user.Email);
        response = await client.GetAsync($"users/getByEmail?email={emailEncoded}");

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

        HttpContext.Session.SetString("UsuarioNome", userResponse.Nome);
        HttpContext.Session.SetString("UsuarioEmail", userResponse.Email);

        // OK
        return RedirectToAction("Index", "Home");
    }
}
