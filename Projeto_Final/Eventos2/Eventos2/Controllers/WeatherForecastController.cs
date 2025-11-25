using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Eventos2.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];

        private readonly IConfiguration _config;

        public WeatherForecastController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet(Name = "privado")]
        [Authorize()]
        public IEnumerable<WeatherForecast> Privado()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("teste")]
        public IActionResult Teste()
        {
            return Ok("Leo");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string model)
        {
            //var user = await _userManager.FindByNameAsync(model.Username);

            //if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
            //    return NotFound("User not found or Invalid credentials");

            //var token = GenerateJwtToken(user);

            //return Ok(new { token });
            // Aqui você valida o usuário do jeito que quiser
            //if (model.Username != "admin" || model.Password != "123")
            //    return Unauthorized("Credenciais inválidas");

            var token = GenerateJwtToken(model);

            return Ok(new { token });
        }

        private string GenerateJwtToken(string user)
        {
            List<Claim> claims = [
                new Claim(ClaimTypes.Name, user)
                ];

            var lkdskl = _config["JWT:Secret"];

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: _config["JWT:Issuer"],
                audience: _config["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(10),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
