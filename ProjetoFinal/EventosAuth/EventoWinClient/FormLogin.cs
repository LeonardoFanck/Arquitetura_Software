using EventosShared.Model;
using EventoWinClient.Context;
using EventoWinClient.Model;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;

namespace EventoWinClient;

public partial class FormLogin : Form
{
	public FormLogin()
	{
		InitializeComponent();

		KeyPreview = true;
		StartPosition = FormStartPosition.CenterScreen;

		KeyDown += FormLogin_KeyDown;
	}

	private void FormLogin_KeyDown(object? sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Escape)
			Close();
	}

	private async void ButtonEntrar_Click(object sender, EventArgs e)
	{
		if (! await ValidarCampos())
			return;

		LoadingManager.ShowLoading(this);
		await Login();
		LoadingManager.HideLoading(this);
	}

	private async Task<bool> ValidarCampos()
	{
		if (string.IsNullOrWhiteSpace(textBoxEmail.Text))
		{
			MessageBox.Show("O campo E-mail é obrigatório.");
			textBoxEmail.Focus();
			return false;
		}

		if (string.IsNullOrWhiteSpace(textBoxSenha.Text))
		{
			MessageBox.Show("O campo Senha é obrigatório.");
			textBoxSenha.Focus();
			return false;
		}

		await Task.CompletedTask;
		return true;
	}

	private async Task Login()
	{
		var login = new UserLogin
		{
			Email = textBoxEmail.Text,
			Senha = textBoxSenha.Text
		};

		var response = await Config.HttpClientAuth.PostAsJsonAsync("Auth/login", login);

		if (!response.IsSuccessStatusCode)
		{
			MessageBox.Show("Login inválido");
			return;
		}

		var token = await response.Content.ReadAsStringAsync();

		if (token is null)
		{
			MessageBox.Show("Token inválido");
			return;
		}

		Config.AuthToken = token;

		Config.HttpClientAuth.DefaultRequestHeaders.Remove("Authorization");
		Config.HttpClientEvento.DefaultRequestHeaders.Remove("Authorization");
		Config.HttpClientCertificado.DefaultRequestHeaders.Remove("Authorization");

		Config.HttpClientAuth.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
		Config.HttpClientEvento.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
		Config.HttpClientCertificado.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

		response = await Config.HttpClientAuth.GetAsync($"Users/GetByEmail?email={textBoxEmail.Text}");

		if (!response.IsSuccessStatusCode)
		{
			MessageBox.Show("Não foi possível obter os dados do usuário.");
			return;
		}

		var user = await response.Content.ReadFromJsonAsync<User>();

		if (user is null)
		{
			MessageBox.Show("Não foi possível obter os dados do usuário.");
			return;
		}

		Config.User = user;

		var formMain = new FormPrincipal();
		Close();

		Thread thread = new(() =>
		{
			Application.Run(formMain);
		});

		thread.Start();
	}
}
