using System.ComponentModel.DataAnnotations;

namespace EventosShared.Model;

public class UserLogin
{
	[Required(ErrorMessage ="O campo email é obrigatório")]
	public string Email { get; set; } = string.Empty;
	[Required(ErrorMessage = "O campo senha é obrigatório")]
	public string Senha { get; set; } = string.Empty;
}
