using EventoWinClient.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EventoWinClient;

public partial class FormEventos : Form
{
    private readonly AppDbContext _dbContext;

    public FormEventos()
    {
        InitializeComponent();

        _dbContext = DbContextFactory.Create();

        Load += FormEventos_Load;
    }

    private async void FormEventos_Load(object? sender, EventArgs e)
    {
        var eventos = await _dbContext.Eventos.ToListAsync();

        GridEventos.DataSource = eventos;

        if(GridEventos.Columns["Id"])
        GridEventos.Columns["Id"].Visible = false;
    }
}
