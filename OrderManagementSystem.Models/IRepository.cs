using System.Linq.Expressions;

namespace OrderManagementSystem.Models;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity?> GetAsync(int id, CancellationToken token);
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken token, Expression<Func<TEntity, bool>>? predicate = null);
    Task AddAsync(TEntity entity, CancellationToken token);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}
