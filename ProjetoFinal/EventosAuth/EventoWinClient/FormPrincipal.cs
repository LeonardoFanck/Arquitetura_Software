using EventosShared.Model;
using EventoWinClient.Context;
using EventoWinClient.Model;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;

namespace EventoWinClient;

public partial class FormPrincipal : Form
{
	public FormPrincipal()
	{
		InitializeComponent();

		StartPosition = FormStartPosition.CenterScreen;
		CheckBoxInternet.Checked = Config.Internet;

		Load += FormPrincipal_Load;
		CheckBoxInternet.CheckedChanged += CheckBoxInternet_CheckedChanged;
	}

	private void CheckBoxInternet_CheckedChanged(object? sender, EventArgs e)
	{
		Config.Internet = CheckBoxInternet.Checked;
	}

	private async void FormPrincipal_Load(object? sender, EventArgs e)
	{
		LoadingManager.ShowLoading(this);
		await SincronizarDados();
		LoadingManager.HideLoading(this);
	}

	private void BtnSair_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void BtnEventos_Click(object sender, EventArgs e)
	{
		var form = new FormEventos();
		form.ShowDialog();
	}

	private void BtnParticipante_Click(object sender, EventArgs e)
	{
		var form = new FormParticipante();
		form.ShowDialog();
	}

	private async void BtnSincronizar_Click(object sender, EventArgs e)
	{
		LoadingManager.ShowLoading(this);
		var resultado = await SincronizarDados();
		LoadingManager.HideLoading(this);

		if (resultado)
			MessageBox.Show("Sincronização concluída com sucesso!");
	}

	private static async Task<bool> SincronizarDados()
	{
		if (!await Config.HasNetworkAsync())
		{
			MessageBox.Show("Sem conexão com internet, não é possível sincronizar os dados no momento!");
			return false;
		}

		var context = DbContextFactory.Create();

		var checkIns = await context.CheckIns.Where(x => !x.Sincronizado).ToListAsync();

		HttpResponseMessage response;

		foreach (var checkInLocal in checkIns)
		{
			var checkIn = new CheckIn()
			{
				Id = checkInLocal.Id,
				EventoId = checkInLocal.EventoId,
				UserId = checkInLocal.UserId,
				DataCheckIn = checkInLocal.DataCheckIn,
			};

			response = await Config.HttpClientEvento.PostAsJsonAsync("checkIn", checkIn);

            if (!response.IsSuccessStatusCode)
			{
				MessageBox.Show("Erro ao sincronizar check-ins. Processo interrompido.");
				return false;
			}

			checkInLocal.Sincronizado = true;
			context.CheckIns.Update(checkInLocal);
			await context.SaveChangesAsync();
		}

		var users = await context.Users.Where(x => !x.Sincronizado).ToListAsync();

		foreach (var userLocal in users)
		{
			var user = new User()
			{
				Id = userLocal.Id,
				Nome = userLocal.Nome,
				Email = userLocal.Email,
				Senha = userLocal.Senha,
				Telefone = userLocal.Telefone,
				CreatedAt = userLocal.CreatedAt,
			};

			response = await Config.HttpClientAuth.PostAsJsonAsync("users", user);

			if (!response.IsSuccessStatusCode)
			{
				MessageBox.Show("Erro ao sincronizar usuários. Processo interrompido.");
				return false;
			}

			userLocal.Sincronizado = true;
			context.Users.Update(userLocal);
			await context.SaveChangesAsync();
		}

		var inscricoes = await context.Inscricaos.Where(x => !x.Sincronizado).ToListAsync();

		foreach (var inscricaoLocal in inscricoes)
		{
			var inscricao = new Inscricao()
			{
				Id = inscricaoLocal.Id,
				EventoId = inscricaoLocal.EventoId,
				UserId = inscricaoLocal.UserId,
				DataInscricao = inscricaoLocal.DataInscricao,
			};

			response = await Config.HttpClientEvento.PostAsJsonAsync("inscricoes", inscricao);

			if (!response.IsSuccessStatusCode)
			{
				MessageBox.Show("Erro ao sincronizar as inscrições. Processo interrompido.");
				return false;
			}

			inscricaoLocal.Sincronizado = true;
			context.Inscricaos.Update(inscricaoLocal);
			await context.SaveChangesAsync();
		}

		response = await Config.HttpClientAuth.GetAsync($"Users/getAll");

		if (!response.IsSuccessStatusCode)
		{
			MessageBox.Show("Não foi possivel buscar os dados dos usuários");
			return false;
		}

		await context.Users.Where(x => x.Sincronizado).ExecuteDeleteAsync();
		context.ChangeTracker.Clear();
		
		await context.Users.AddRangeAsync(await response.Content.ReadFromJsonAsync<List<UserLocal>>() ?? []);
		await context.SaveChangesAsync();

		response = await Config.HttpClientEvento.GetAsync($"Eventos/getAllavailable");

		if (!response.IsSuccessStatusCode)
		{
			MessageBox.Show("Não foi possivel buscar os dados dos eventos");
			return false;
		}

		await context.Eventos.Where(x => x.Sincronizado).ExecuteDeleteAsync();
		context.ChangeTracker.Clear();

		await context.Eventos.AddRangeAsync(await response.Content.ReadFromJsonAsync<List<EventoLocal>>() ?? []);
		await context.SaveChangesAsync();

		response = await Config.HttpClientEvento.GetAsync($"CheckIn/getAll");

		if (!response.IsSuccessStatusCode)
		{
			MessageBox.Show("Não foi possivel buscar os dados dos check-ins");
			return false;
		}

		await context.CheckIns.Where(x => x.Sincronizado).ExecuteDeleteAsync();
		context.ChangeTracker.Clear();

		await context.CheckIns.AddRangeAsync(await response.Content.ReadFromJsonAsync<List<CheckInLocal>>() ?? []);
		await context.SaveChangesAsync();

		response = await Config.HttpClientEvento.GetAsync($"Inscricoes/getAll");
		if (!response.IsSuccessStatusCode)
		{
			MessageBox.Show("Não foi possivel buscar os dados das inscrições");
			return false;
		}

		await context.Inscricaos.Where(x => x.Sincronizado).ExecuteDeleteAsync();
		context.ChangeTracker.Clear();

		await context.Inscricaos.AddRangeAsync(await response.Content.ReadFromJsonAsync<List<InscricaoLocal>>() ?? []);
		await context.SaveChangesAsync();

		var leo = await context.Eventos.ToListAsync();

		return true;
	}
}
