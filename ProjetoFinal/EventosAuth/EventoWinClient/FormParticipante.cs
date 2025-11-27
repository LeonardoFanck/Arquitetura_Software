using EventosShared.Model;
using EventoWinClient.Context;
using EventoWinClient.Model;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;

namespace EventoWinClient;

public partial class FormParticipante : Form
{
	private readonly AppDbContext _context = DbContextFactory.Create();
	private List<UserLocal> _usuarios = [];

	public FormParticipante()
	{
		InitializeComponent();

		KeyPreview = true;
		StartPosition = FormStartPosition.CenterScreen;
		KeyDown += FormParticipante_KeyDown;
		Load += FormParticipante_Load;
	}

	private async void FormParticipante_Load(object? sender, EventArgs e)
	{
		_usuarios = await _context.Users.ToListAsync();
	}

	private async void ButtonGravar_Click(object sender, EventArgs e)
	{
		if(string.IsNullOrWhiteSpace(TxtEmail.Text))
		{
			MessageBox.Show("O campo E-mail é obrigatório.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			TxtEmail.Focus();
			return;
		}

		if(string.IsNullOrWhiteSpace(TxtSenha.Text))
		{
			MessageBox.Show("O campo Senha é obrigatório.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			TxtSenha.Focus();
			return;
		}

		if(_usuarios.Any(u => u.Email.Equals(TxtEmail.Text, StringComparison.OrdinalIgnoreCase)))
		{
			MessageBox.Show("Já existe um usuário com este e-mail.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			TxtEmail.Focus();
			return;
		}

		var userLocal = new UserLocal()
		{
			Id = Guid.NewGuid(),
			Nome = "",
			Email = TxtEmail.Text,
			Senha = TxtSenha.Text,
			Telefone = "",
			CreatedAt = DateTime.Now,
			Sincronizado = false,
		};
		
		await _context.Users.AddAsync(userLocal);
		await _context.SaveChangesAsync();

		if(await Config.HasNetworkAsync() == false)
		{
			MessageBox.Show("Usuário salvo localmente. Sem conexão de rede no momento.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
			Close();
			return;
		}

		var user = new User()
		{
			Id = userLocal.Id,
			Nome = userLocal.Nome,
			Email = userLocal.Email,
			Senha = userLocal.Senha,
			Telefone = userLocal.Telefone,
			CreatedAt = userLocal.CreatedAt,
		};

		var response = await Config.HttpClientAuth.PostAsJsonAsync("users", user);

		if (!response.IsSuccessStatusCode)
		{
			MessageBox.Show("Erro ao sincronizar usuário com o servidor.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
			Close();
			return;
		}

		//var newUser = await response.Content.ReadFromJsonAsync<User>();

		userLocal.Sincronizado = true;

		_context.Users.Update(userLocal);
		await _context.SaveChangesAsync();

		MessageBox.Show("Usuário salvo e sincronizado com o servidor.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

		Close();

	}

	private void ButtonCancelar_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void FormParticipante_KeyDown(object? sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Escape)
			Close();
	}
}
