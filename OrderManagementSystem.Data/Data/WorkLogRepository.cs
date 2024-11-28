using Azure.Core;
using Domain.WorkLogs;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class WorkLogRepository : Repository<WorkLog>, IWorkLogRepository
{
    private readonly AppDbContext _dbContext;

    public WorkLogRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<WorkLog>> GetAllAsync(CancellationToken token)
    {
        return await _dbContext.WorkLogs
            .Include(w => w.Customer)
            .Include(w => w.WorkType)
            .ToArrayAsync(token);
    }

    public new async Task<WorkLog?> GetAsync(int id, CancellationToken token)
    {
        return await _dbContext.WorkLogs
            .Where(w => w.Id == id)
            .Include(w => w.Customer)
            .Include(w => w.WorkType)
            .FirstOrDefaultAsync(token);
    }
}