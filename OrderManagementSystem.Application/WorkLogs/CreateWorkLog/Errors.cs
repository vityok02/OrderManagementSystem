using Domain.Abstract;

namespace Application.WorkLogs.CreateWorkLog;

public static class CreateWorkLogErrors
{
    public static readonly Func<int, Error> WorkTypeNotFound = id
        => new("WorkType.NotFound", $"Work type with id {id} not found.");

    public static readonly Error WorkTypesEmpty
        = new("WorkTypes.Empty", "Work types are empty.");
}
