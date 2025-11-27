using EventosShared.Model;
using EventoWinClient.Context;
using EventoWinClient.Model;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EventoWinClient;

public partial class FormAdicionarUsuarioEvento : Form
{
	private readonly AppDbContext _dbContext = DbContextFactory.Create();
	private readonly EventoLocal _eventoLocal;
	private List<UserLocal> _usuarios = [];

	public FormAdicionarUsuarioEvento(EventoLocal eventoLocal)
	{
		InitializeComponent();

		_eventoLocal = eventoLocal;

		KeyPreview = true;
		StartPosition = FormStartPosition.CenterScreen;
		KeyDown += FormAdicionarUsuarioEvento_KeyDown;
		Load += FormAdicionarUsuarioEvento_Load;

		GridUsuarios.ReadOnly = true;
		GridUsuarios.MultiSelect = false;
		GridUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
		GridUsuarios.CellDoubleClick += GridUsuarios_CellDoubleClick;
	}

	private async void GridUsuarios_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
	{
		var usuario = _usuarios[e.RowIndex];

		if (MessageBox.Show($"Deseja adicionar o usuário '{usuario.Email}' ao evento '{_eventoLocal.Titulo}'?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
			return;

		var inscricaoLocal = new InscricaoLocal
		{
			EventoId = _eventoLocal.Id,
			UserId = usuario.Id,
			DataInscricao = DateTime.Now,
			Id = Guid.NewGuid(),
			Sincronizado = false,
		};

		await _dbContext.Inscricaos.AddAsync(inscricaoLocal);

		var checkInLocal = new CheckInLocal()
		{
			Id = Guid.NewGuid(),
			DataCheckIn = DateTime.Now,
			EventoId = _eventoLocal.Id,
			UserId = usuario.Id,
			Sincronizado = false,
		};

		await _dbContext.CheckIns.AddAsync(checkInLocal);

		await _dbContext.SaveChangesAsync();

		if (await Config.HasNetworkAsync() == false)
		{
			MessageBox.Show("Inscrição salva localmente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
			Close();
			return;
		}

		var inscricao = new Inscricao()
		{
			DataInscricao = inscricaoLocal.DataInscricao,
			EventoId = inscricaoLocal.EventoId,
			Id = inscricaoLocal.Id,
			UserId = inscricaoLocal.UserId,
		};

		var response = await Config.HttpClientEvento.PostAsJsonAsync("inscricoes", inscricao);

		if (!response.IsSuccessStatusCode)
		{
			MessageBox.Show("Erro ao sincronizar inscrição com o servidor.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
			return;
		}

		//var inscricaoSincronizada = await response.Content.ReadFromJsonAsync<Inscricao>();

		var checkIn = new CheckIn()
		{
			DataCheckIn = checkInLocal.DataCheckIn,
			EventoId = checkInLocal.EventoId,
			Id = checkInLocal.Id,
			UserId = checkInLocal.UserId,
		};

		response = await Config.HttpClientEvento.PostAsJsonAsync("checkIn", checkIn);

		if (!response.IsSuccessStatusCode)
		{
			MessageBox.Show("Erro ao sincronizar inscrição com o servidor.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
			return;
		}

		var checkInSincronizado = await response.Content.ReadFromJsonAsync<CheckIn>();

		inscricaoLocal.Sincronizado = true;
		_dbContext.Inscricaos.Update(inscricaoLocal);

		checkInLocal.Sincronizado = true;
		_dbContext.CheckIns.Update(checkInLocal);

		await _dbContext.SaveChangesAsync();






		MessageBox.Show($"Usuário '{usuario.Email}' adicionado ao evento '{_eventoLocal.Titulo}' com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

		Close();
	}

	private async void FormAdicionarUsuarioEvento_Load(object? sender, EventArgs e)
	{
		Text += " - " + _eventoLocal.Titulo;

		await CarregarGrid();
	}

	private void FormAdicionarUsuarioEvento_KeyDown(object? sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Escape)
			Close();
	}

	private async Task CarregarGrid()
	{
		var usuarios = await _dbContext.Users.ToListAsync();

		var eventos = await _dbContext.Eventos.ToListAsync();

		var usuariosInscritos = from evento in eventos
								join inscricao in await _dbContext.Inscricaos.ToListAsync() on evento.Id equals inscricao.EventoId
								join user in usuarios on inscricao.UserId equals user.Id
								where evento.Id == _eventoLocal.Id
								select user.Id;

		_usuarios = [.. usuarios.Where(u => !usuariosInscritos.Contains(u.Id))];

		GridUsuarios.DataSource = _usuarios;

		GridUsuarios.Columns["Id"].Visible = false;
		GridUsuarios.Columns["Sincronizado"].Visible = false;
		GridUsuarios.Columns["CreatedAt"].Visible = false;

	}
}
