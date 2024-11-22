using Application.Abstract.Interfaces;

namespace Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public async Task SaveChanges(CancellationToken token)
    {
        await _context.SaveChangesAsync(token);
    }
}
