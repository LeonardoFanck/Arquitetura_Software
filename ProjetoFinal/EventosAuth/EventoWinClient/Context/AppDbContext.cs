using EventoWinClient.Model;
using Microsoft.EntityFrameworkCore;

namespace EventoWinClient.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
	public DbSet<UserLocal> Users { get; set; }
	public DbSet<EventoLocal> Eventos { get; set; }
	public DbSet<CheckInLocal> CheckIns { get; set; }
	public DbSet<InscricaoLocal> Inscricaos { get; set; }
}
