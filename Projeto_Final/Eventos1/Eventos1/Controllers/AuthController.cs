using Eventos1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Eventos1.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(/*UserManager<IdentityUser> userManager,*/ IConfiguration config) : Controller
{
    //private readonly UserManager<IdentityUser> _userManager = userManager;
    private readonly IConfiguration _config = config;

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterModel model)
    {
        //var user = new IdentityUser
        //{
        //    UserName = model.Username,
        //    Email = model.Email
        //};

        //var result = await _userManager.CreateAsync(user, model.Password);

        //if (!result.Succeeded)
        //    return BadRequest(result.Errors);

        return Ok("User registered successfully");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginModel model)
    {
        //var user = await _userManager.FindByNameAsync(model.Username);

        //if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
        //    return NotFound("User not found or Invalid credentials");

        //var token = GenerateJwtToken(user);

        //return Ok(new { token });
        // Aqui você valida o usuário do jeito que quiser
        if (model.Username != "admin" || model.Password != "123")
            return Unauthorized("Credenciais inválidas");

        var token = GenerateJwtToken(model);

        return Ok(new { token });
    }

    private string GenerateJwtToken(LoginModel user)
    {
        List<Claim> claims = [
            new Claim(ClaimTypes.Name, user.Username)
            ];

        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["JWT:Secret"]!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new JwtSecurityToken(
            issuer: _config["JWT:Issuer"],
            audience: _config["JWT:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(10),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }

}
