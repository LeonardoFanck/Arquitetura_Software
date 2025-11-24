using EventosShared.Context;
using EventosShared.Interfaces;
using EventosShared.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EventosAuth.Repository;

public class UserRepository(AppDbContext context) : IRepository<User>
{
	private readonly AppDbContext _context = context;

	public async Task<IEnumerable<User>> GetAllAsync()
	{
		return await _context.Users.ToListAsync();
	}

	public async Task<User?> GetByIdAsync(Guid id, bool noTracking = false)
	{
		if(noTracking)
			return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);

		return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
	}

	public async Task CreateAsync(User user)
	{
		await _context.Users.AddAsync(user);
		await _context.SaveChangesAsync();
	}

	public async Task UpdateAsync(User user)
	{
		_context.Users.Update(user);
		await _context.SaveChangesAsync();
	}

	public async Task DeleteAsync(Guid id)
	{
		var user = await GetByIdAsync(id);

		if(user is null)
			return;

		_context.Users.Remove(user);
		await _context.SaveChangesAsync();
	}

	public async Task<User?> GetByExpression(Expression<Func<User, bool>> expression, bool noTracking = false)
	{
		if(noTracking)
			return await _context.Users.AsNoTracking().FirstOrDefaultAsync(expression);

		return await _context.Users.FirstOrDefaultAsync(expression);
	}
}
