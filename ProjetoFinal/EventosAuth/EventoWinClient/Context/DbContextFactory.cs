using Microsoft.EntityFrameworkCore;

namespace EventoWinClient.Context;

public static class DbContextFactory
{
    private static string _connectionString = "Data Source=evento.db;Cache=Shared";

    public static AppDbContext Create()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().UseSqlite(_connectionString).Options;
        
        var context = new AppDbContext(options);

        //context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        return context;
    }
}
