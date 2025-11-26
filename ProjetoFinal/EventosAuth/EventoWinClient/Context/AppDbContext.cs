using EventosShared.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventoWinClient.Context;

public class AppDbContext : DbContext
{
	public DbSet<User> Users { get; set; }
	public DbSet<Evento> Eventos { get; set; }
	public DbSet<CheckIn> CheckIns { get; set; }
	public DbSet<Inscricao> Inscricaos { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		base.OnConfiguring(optionsBuilder);

		optionsBuilder.UseSqlite("Data Source=evento.db");
	}
}
