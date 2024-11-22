using OrderManagementSystem.Models;

namespace Domain.WorkLogs;

public class WorkLogUpdater
{
    private readonly IRepository<WorkLog> _workLogRepository;

    public WorkLogUpdater(IRepository<WorkLog> workLogRepository)
    {
        _workLogRepository = workLogRepository;
    }

    public async Task MarkAsCompleted(int id, CancellationToken token)
    {
        var workLog = await _workLogRepository
            .GetAsync(id, token);

        workLog!.SetStatus(Status.Paid);
    }

    public async Task MarkAsCanceled(int id, CancellationToken token)
    {
        var workLog = await _workLogRepository
            .GetAsync(id, token);

        workLog!.SetStatus(Status.Cancelled);
    }

    public async Task UpdateTotalPrice(int id, decimal price, CancellationToken token)
    {
        var workLog = await _workLogRepository.GetAsync(id, token);

        workLog!.UpdatePrice(workLog.TotalPrice - price);
        workLog.SetStatus(Status.PaidPartially);
    }
}