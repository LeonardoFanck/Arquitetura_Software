using EventosShared.Context;
using EventosShared.Interfaces;
using EventosShared.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace EventosEvento.Repository;

public class EventoRepository(AppDbContext context) : IRepository<Evento>
{
	private readonly AppDbContext _context = context;

	public Task CreateAsync(Evento entity)
	{
		_context.Eventos.Add(entity);
		return _context.SaveChangesAsync();
	}

	public async Task DeleteAsync(Guid id)
	{
		var evento = await _context.Eventos.FindAsync(id);

		if (evento is null)
			return;

		_context.Eventos.Remove(evento);
		await _context.SaveChangesAsync();
	}

	public async Task<IEnumerable<Evento>> GetAllAsync()
	{
		return await _context.Eventos.ToListAsync();
	}

	public async Task<IEnumerable<Evento>> GetAllByExpression(Expression<Func<Evento, bool>> expression, bool noTracking = false)
	{
		if (noTracking)
			return await _context.Eventos.AsNoTracking().Where(expression).ToListAsync();

		return await _context.Eventos.Where(expression).ToListAsync();
	}

	public async Task<Evento?> GetByExpression(Expression<Func<Evento, bool>> expression, bool noTracking = false)
	{
		return noTracking
			? await _context.Eventos.AsNoTracking().FirstOrDefaultAsync(expression)
			: await _context.Eventos.FirstOrDefaultAsync(expression);
	}

	public async Task<Evento?> GetByIdAsync(Guid id, bool noTracking = false)
	{
		return noTracking
			? await _context.Eventos.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id)
			: await _context.Eventos.FirstOrDefaultAsync(e => e.Id == id);
	}

	public async Task UpdateAsync(Evento entity)
	{
		_context.Eventos.Update(entity);
		await _context.SaveChangesAsync();
	}
}
