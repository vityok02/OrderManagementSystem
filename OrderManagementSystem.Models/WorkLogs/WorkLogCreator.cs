using OrderManagementSystem.Models;

namespace Domain.WorkLogs;

public class WorkLogCreator
{
    private readonly IRepository<WorkLog> _workLogRepository;

    public WorkLogCreator(IRepository<WorkLog> workLogRepository)
    {
        _workLogRepository = workLogRepository;
    }

    public async Task<WorkLog> CreateWorkLog(Customer customer, WorkType workType, int amount, decimal unitPrice,
        CancellationToken token)
    {
        var workLog = new WorkLog(customer, workType, amount, unitPrice);
        await _workLogRepository.AddAsync(workLog, token);

        return workLog;
    }
}
