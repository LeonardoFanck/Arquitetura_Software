using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EventosAuth.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(IConfiguration config) : Controller
{
    private readonly IConfiguration _config = config;

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("login")]
    public async Task<IActionResult> Login()
    {
        List<Claim> claims = [
           new Claim(ClaimTypes.Name, "leo")
           ];

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new JwtSecurityToken(
            issuer: _config["JWT:Issuer"],
            audience: _config["JWT:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(10),
            signingCredentials: creds
        );

        var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

        return Ok(token);
    }
}
