using Domain.WorkLogs;

namespace Application.WorkLogs;

public static class WorkLogExtensions
{
    public static WorkLogDto ToDto(this WorkLog workLog)
    {
        // TODO: include in repository
        return new WorkLogDto(
            workLog.Id,
            workLog.Customer.Name,
            workLog.Customer.PhoneNumber,
            workLog.WorkType.Name,
            workLog.Amount,
            workLog.UnitPrice,
            workLog.TotalPrice,
            workLog.Status);
    }
}