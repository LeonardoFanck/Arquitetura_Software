using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eventos2.Controllers;

[ApiController]
[Route("[controller]")]
public class TesteController : Controller
{
    //[HttpGet("ping")]
    [HttpGet("ping")]
    public IActionResult Ping()
    {
        return Ok("pong");
    }

    [Authorize]
    [HttpGet]
    public IActionResult Leo()
    {
        return Ok("Leo");
    }
}
