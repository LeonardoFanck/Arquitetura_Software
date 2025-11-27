using EventosShared.Model;
using EventoWinClient.Context;
using EventoWinClient.Model;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EventoWinClient;

public partial class FormEventoDetalhes : Form
{
	private readonly EventoLocal _evento;
	private readonly AppDbContext _dbContext;
	private List<InscricaoLocal> _inscricoes = [];
	private List<UserLocal> _usuarios = [];
	private List<CheckInLocal> _checkIns = [];

	public FormEventoDetalhes(EventoLocal evento)
	{
		InitializeComponent();
		_evento = evento;
		_dbContext = DbContextFactory.Create();

		KeyPreview = true;
		StartPosition = FormStartPosition.CenterScreen;
		Load += FormEventoDetalhes_Load;
		KeyDown += FormEventoDetalhes_KeyDown;

		GridInscricoes.CellDoubleClick += GridInscricoes_CellDoubleClick;
		GridInscricoes.ReadOnly = true;
		GridInscricoes.MultiSelect = false;
		GridInscricoes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
	}

	private void FormEventoDetalhes_KeyDown(object? sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Escape)
			Close();
	}

	private async void GridInscricoes_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
	{
		var inscricao = _inscricoes[e.RowIndex];
		var usuario = _usuarios.First(x => x.Id == inscricao.UserId);
		var checkInExistente = _checkIns.FirstOrDefault(x => x.UserId == usuario.Id);

		if (checkInExistente is not null)
		{
			MessageBox.Show("Usuário já realizou o check-in: " + usuario.Email);
			return;
		}

		if (MessageBox.Show("Confirmar o checkIn para o usuário: " + usuario.Nome + "?", "Pergunta", MessageBoxButtons.YesNo) != DialogResult.Yes)
			return;

		var checkInLocal = new CheckInLocal
		{
			DataCheckIn = DateTime.Now,
			EventoId = _evento.Id,
			Id = Guid.NewGuid(),
			UserId = usuario.Id,
			Sincronizado = false
		};

		var context = DbContextFactory.Create();

		await context.CheckIns.AddAsync(checkInLocal);
		await context.SaveChangesAsync();


		if (await Config.HasNetworkAsync() == false)
			return;

		CheckIn checkIn = checkInLocal;

		var response = await Config.HttpClientEvento.PostAsJsonAsync("checkIn", checkIn);

		if (!response.IsSuccessStatusCode)
		{
			MessageBox.Show("Erro ao registrar check-in: " + response.ReasonPhrase);
			return;
		}

		checkInLocal.Sincronizado = true;

		context.CheckIns.Update(checkInLocal);

		await context.SaveChangesAsync();

		MessageBox.Show("Check-in registrado com sucesso para o usuário: " + usuario.Nome);
	}

	private async void FormEventoDetalhes_Load(object? sender, EventArgs e)
	{
		await CarregarGrid();
	}

	private async Task CarregarGrid()
	{
		_inscricoes = await _dbContext.Inscricaos.Where(x => x.EventoId == _evento.Id).ToListAsync();
		var usuarioIds = _inscricoes.Select(x => x.UserId).ToList();

		_usuarios = await _dbContext.Users.Where(x => usuarioIds.Contains(x.Id)).ToListAsync();

		_checkIns = await _dbContext.CheckIns.Where(x => x.EventoId == _evento.Id).ToListAsync();


		var dadosGrid = (from inscricao in _inscricoes
						 join usuario in _usuarios on inscricao.UserId equals usuario.Id
						 select new
						 {
							 usuario.Nome,
							 usuario.Email,
							 usuario.Telefone,
							 inscricao.DataInscricao,
							 CheckIn = _checkIns.FirstOrDefault(x => x.UserId == usuario.Id) is not null
						 }).OrderBy(x => x.CheckIn == false).ThenBy(x => x.Email).ToList();

		GridInscricoes.DataSource = dadosGrid;
	}

	private async void BtnAdicionar_Click(object sender, EventArgs e)
	{
		FormAdicionarUsuarioEvento form = new(_evento);
	 	form.ShowDialog();

		await CarregarGrid();
	}
}
