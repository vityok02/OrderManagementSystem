using Domain.Abstract;

namespace Application.WorkLogs;
public class WorkLogErrors
{
    public static readonly Func<int, Error> WorkTypeNotFound = id
        => new("WorkType.NotFound", $"Work type with id {id} not found.");

    public static readonly Error WorkTypesEmpty
        = new("WorkTypes.Empty", "Work types are empty.");

    public static readonly Func<int, Error> WorkLogNotFound = id
        => new("WorkLog.NotFound", $"Work type with id {id} not found.");

    public static readonly Error InvalidStatus
        = new("WorkType.InvalidStatus", "Invalid status");
}
