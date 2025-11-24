namespace EventosShared.Model;

public class User
{
	public Guid Id { get; set; }
	public string Nome { get; set; } = "";
	public string Email { get; set; } = "";
	public string Senha { get; set; } = "";
	public string Telefone { get; set; } = "";

	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}