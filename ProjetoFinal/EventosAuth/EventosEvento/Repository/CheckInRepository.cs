using EventosShared.Context;
using EventosShared.Interfaces;
using EventosShared.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EventosEvento.Repository;

public class CheckInRepository(AppDbContext context) : IRepository<CheckIn>
{
	private readonly AppDbContext _context = context;

	public Task CreateAsync(CheckIn checkIn)
	{
		_context.CheckIns.Add(checkIn);
		return _context.SaveChangesAsync();
	}

	public async Task DeleteAsync(Guid id)
	{
		var checkIn = await _context.CheckIns.FindAsync(id);

		if (checkIn is null)
			return;

		_context.CheckIns.Remove(checkIn);
		await _context.SaveChangesAsync();
	}

	public async Task<IEnumerable<CheckIn>> GetAllAsync()
	{
		return await _context.CheckIns.ToListAsync();
	}

	public async Task<CheckIn?> GetByExpression(Expression<Func<CheckIn, bool>> expression, bool noTracking = false)
	{
		return noTracking
			? await _context.CheckIns.AsNoTracking().FirstOrDefaultAsync(expression)
			: await _context.CheckIns.FirstOrDefaultAsync(expression);
	}

	public async Task<CheckIn?> GetByIdAsync(Guid id, bool noTracking = false)
	{
		return noTracking
			? await _context.CheckIns.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id)
			: await _context.CheckIns.FirstOrDefaultAsync(e => e.Id == id);
	}

	public async Task UpdateAsync(CheckIn entity)
	{
		_context.CheckIns.Update(entity);
		await _context.SaveChangesAsync();
	}
}
