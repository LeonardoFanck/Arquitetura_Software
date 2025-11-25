using System.Linq.Expressions;

namespace EventosShared.Interfaces;

public interface IRepository<T>
{
	public Task<IEnumerable<T>> GetAllAsync();
	public Task<T?> GetByIdAsync(Guid id, bool noTracking = false);
	public Task<T?> GetByExpression(Expression<Func<T, bool>> expression, bool noTracking = false);
	public Task<IEnumerable<T>> GetAllByExpression(Expression<Func<T, bool>> expression, bool noTracking = false);
	public Task CreateAsync(T entity);
	public Task UpdateAsync(T entity);
	public Task DeleteAsync(Guid id);
}
