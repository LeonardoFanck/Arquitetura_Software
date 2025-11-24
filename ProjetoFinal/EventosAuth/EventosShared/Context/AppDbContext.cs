using EventosShared.Model;
using Microsoft.EntityFrameworkCore;

namespace EventosShared.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
	public DbSet<User> Users { get; set; }
	public DbSet<Evento> Eventos { get; set; }
	public DbSet<CheckIn> CheckIns { get; set; }	
	public DbSet<Inscricao> Inscricoes { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<User>().Property(x => x.CreatedAt).ValueGeneratedOnAdd().Metadata.SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore);

		base.OnModelCreating(modelBuilder);
	}
}
