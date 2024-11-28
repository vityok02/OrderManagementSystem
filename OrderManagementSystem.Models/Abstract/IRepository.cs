using System.Linq.Expressions;

namespace Domain.Abstract;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity?> GetAsync(int id, CancellationToken token);
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken token, Expression<Func<TEntity, bool>>? predicate = null);
    Task CreateAsync(TEntity entity, CancellationToken token);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task SaveChangesAsync(CancellationToken token);
}
