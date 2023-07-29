using System.Linq.Expressions;

namespace OrderManagementSystem.Models;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity?> GetAsync(int id);
    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null);
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task SaveChangesAsync();
}
