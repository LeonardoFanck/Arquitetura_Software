using EventoWinClient.Context;
using EventoWinClient.Model;
using Microsoft.EntityFrameworkCore;

namespace EventoWinClient;

public partial class FormEventos : Form
{
	private readonly AppDbContext _dbContext;
	private List<EventoLocal> _eventos = [];

	public FormEventos()
	{
		InitializeComponent();

		_dbContext = DbContextFactory.Create();

		KeyPreview = true;
		StartPosition = FormStartPosition.CenterScreen;
		Load += FormEventos_Load;
		KeyDown += FormEventos_KeyDown;

		GridEventos.CellDoubleClick += GridEventos_CellDoubleClick;
		GridEventos.ReadOnly = true;
		GridEventos.MultiSelect = false;
		GridEventos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
	}

	private void FormEventos_KeyDown(object? sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Escape)
			Close();
	}

	private void GridEventos_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
	{
		var evento = _eventos[e.RowIndex];

		FormEventoDetalhes formEventoDetalhes = new(evento);
		formEventoDetalhes.ShowDialog();
	}

	private async void FormEventos_Load(object? sender, EventArgs e)
	{
		_eventos = await _dbContext.Eventos.ToListAsync();

		GridEventos.DataSource = _eventos;

		GridEventos.Columns["Id"].Visible = false;
		GridEventos.Columns["Sincronizado"].Visible = false;
	}
}
