using EventosShared.Model;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EventoWinClient;

public partial class FormLogin : Form
{
	public FormLogin()
	{
		InitializeComponent();
	}

	private async void ButtonEntrar_Click(object sender, EventArgs e)
	{
		var login = new UserLogin
		{
			Email = textBoxEmail.Text,
			Senha = textBoxSenha.Text
		};

		var response = await Config.HttpClientAuth.PostAsJsonAsync("Auth/login", login);

		if(!response.IsSuccessStatusCode)
		{
			MessageBox.Show("Login inválido");
			return;
		}

		var token = await response.Content.ReadAsStringAsync();

		if(token is null)
		{
			MessageBox.Show("Token inválido");
			return;
		}

		Config.AuthToken = token;

		var formMain = new FormPrincipal();
		Close();
		
		Thread thread = new(() =>
		{
			Application.Run(formMain);
		});

		thread.Start();
	}
}
