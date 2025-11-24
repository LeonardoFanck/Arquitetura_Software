using EventosEvento.Repository;
using EventosShared;
using EventosShared.Context;
using EventosShared.Interfaces;
using EventosShared.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
{
	options.UseSqlServer(Config.ConnectionString);
});

builder.Services.AddAuthentication(x =>
{
	x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
	options.RequireHttpsMetadata = false;
	options.SaveToken = true;
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = false,
		ValidateAudience = false,
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Config.JwtSecretKey))
	};
});

builder.Services.AddAuthorization();

builder.Services.AddControllers();

builder.Services.AddScoped<IRepository<Evento>, EventoRepository>();
builder.Services.AddScoped<IRepository<Inscricao>, InscricaoRepository>();
builder.Services.AddScoped<IRepository<CheckIn>, CheckInRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
