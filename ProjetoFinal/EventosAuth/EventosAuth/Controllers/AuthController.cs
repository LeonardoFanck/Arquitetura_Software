using EventosShared;
using EventosShared.Interfaces;
using EventosShared.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EventosAuth.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(IConfiguration configuration, IRepository<User> repository) : ControllerBase
{
	private readonly IConfiguration _configuration = configuration;
	private readonly IRepository<User> _repository = repository;	

	[HttpPost("login")]
	public async Task<IActionResult> Login(UserLogin userLogin)
	{
		if (!ModelState.IsValid)
			return NotFound("Dados inválidos para a solicitação");
		
		var user = await _repository.GetByExpression(x => x.Email == userLogin.Email && x.Senha == userLogin.Senha);

		if(user is null)
			return NotFound("Usuário ou senha inválidos.");

		var token = GerarToken(user);

		return Ok(token);
	}

	private static string GerarToken(User user)
	{
		var tokenHandler = new JwtSecurityTokenHandler();
		var key = Encoding.ASCII.GetBytes(Config.JwtSecretKey);
		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity([
				new Claim(ClaimTypes.Name, user.Nome),
				new Claim("userID", user.Id.ToString())
					]),
			Expires = DateTime.UtcNow.AddHours(8),
			SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
		};

		var token = tokenHandler.CreateToken(tokenDescriptor);

		return tokenHandler.WriteToken(token);
	}
}
