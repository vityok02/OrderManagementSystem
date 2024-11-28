using Domain.Abstract;

namespace Domain.WorkLogs;

public interface IWorkLogRepository : IRepository<WorkLog>
{
    Task<IEnumerable<WorkLog>> GetAllAsync(CancellationToken token);
    new Task<WorkLog?> GetAsync(int id, CancellationToken token);
}
