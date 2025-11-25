using EventosShared.Context;
using EventosShared.Interfaces;
using EventosShared.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EventosEvento.Repository;

public class InscricaoRepository(AppDbContext context) : IRepository<Inscricao>
{
	private readonly AppDbContext _context = context;

	public async Task CreateAsync(Inscricao entity)
	{
		_context.Inscricoes.Add(entity);

		var evento = await _context.Eventos.FindAsync(entity.EventoId);

		if (evento is not null)
		{
			evento.VagasUtilizadas = Math.Min(evento.Vagas, evento.VagasUtilizadas + 1);
			_context.Eventos.Update(evento);
		}

		await _context.SaveChangesAsync();
	}

	public async Task DeleteAsync(Guid id)
	{
		var inscricao = await _context.Inscricoes.FindAsync(id);

		if (inscricao is null)
			return;

		var evento = await _context.Eventos.FindAsync(inscricao.EventoId);

		if(evento is not null)
		{
			evento.VagasUtilizadas = Math.Max(0, evento.VagasUtilizadas - 1);
			_context.Eventos.Update(evento);
		}

		_context.Inscricoes.Remove(inscricao);
		await _context.SaveChangesAsync();
	}

	public async Task<IEnumerable<Inscricao>> GetAllAsync()
	{
		return await _context.Inscricoes.ToListAsync();
	}

	public async Task<IEnumerable<Inscricao>> GetAllByExpression(Expression<Func<Inscricao, bool>> expression, bool noTracking = false)
	{
		if (noTracking)
			return await _context.Inscricoes.AsNoTracking().Where(expression).ToListAsync();

		return await _context.Inscricoes.Where(expression).ToListAsync();
	}

	public async Task<Inscricao?> GetByExpression(Expression<Func<Inscricao, bool>> expression, bool noTracking = false)
	{
		return noTracking
			? await _context.Inscricoes.AsNoTracking().FirstOrDefaultAsync(expression)
			: await _context.Inscricoes.FirstOrDefaultAsync(expression);
	}

	public async Task<Inscricao?> GetByIdAsync(Guid id, bool noTracking = false)
	{
		return noTracking
			? await _context.Inscricoes.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id)
			: await _context.Inscricoes.FirstOrDefaultAsync(e => e.Id == id);
	}

	public async Task UpdateAsync(Inscricao inscrico)
	{
		_context.Inscricoes.Update(inscrico);
		await _context.SaveChangesAsync();
	}
}
