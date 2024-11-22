using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Models;
using System.Linq.Expressions;

namespace Infrastructure.Data;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    private readonly AppDbContext _dbContext;
    public Repository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(TEntity entity, CancellationToken token)
    {
        await _dbContext.Set<TEntity>().AddAsync(entity, token);
    }

    public void Update(TEntity entity)
    {
        _dbContext.Update(entity);
    }

    public async Task<TEntity?> GetAsync(int id, CancellationToken token)
    {
        var entity = await _dbContext
            .Set<TEntity>()
            .FindAsync(id, token);

        return entity;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(
        CancellationToken token, 
        Expression<Func<TEntity, bool>>? predicate = null)
    {
        IQueryable<TEntity> entities = _dbContext
            .Set<TEntity>();

        if (predicate is not null)
        {
            entities = entities.Where(predicate);
        }

        return await entities.ToArrayAsync(token);
    }

    public void Delete(TEntity entity)
    {
        _dbContext.Set<TEntity>().Remove(entity);
    }
}
