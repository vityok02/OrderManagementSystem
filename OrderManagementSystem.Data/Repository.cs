using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Models;
using System.Linq.Expressions;

namespace OrderManagementSystem.Data;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    private readonly AppDbContext _dbContext;
    public Repository(AppDbContext dbContext) 
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(TEntity entity)
    {
        await _dbContext.Set<TEntity>().AddAsync(entity);
        await SaveChangesAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _dbContext.Update(entity);
        await SaveChangesAsync();
    }

    public async Task<TEntity?> GetAsync(int id)
    {
        var entity = await _dbContext.Set<TEntity>().FindAsync(id);

        return entity;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null)
    {
        IQueryable<TEntity> entities = _dbContext.Set<TEntity>();

        if (predicate is not null)
        {
            entities = entities.Where(predicate);
        }

        return await entities.ToArrayAsync();
    }

    public async Task DeleteAsync(TEntity entity)
    {
        _dbContext.Set<TEntity>().Remove(entity);
        await SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
