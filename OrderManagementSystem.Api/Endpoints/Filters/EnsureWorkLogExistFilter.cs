using Application.WorkLogs;
using Domain.WorkLogs;

namespace Api.Endpoints.Filters;

public class EnsureWorkLogExistFilter : IEndpointFilter
{
    private readonly IWorkLogRepository _workLogRepository;

    public EnsureWorkLogExistFilter(IWorkLogRepository workLogRepository)
    {
        _workLogRepository = workLogRepository;
    }

    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext efiContext,
        EndpointFilterDelegate next)
    {
        int.TryParse(
            efiContext.HttpContext.Request.RouteValues["id"]?.ToString(),
            out int id);

        var cancellationToken = efiContext
            .HttpContext
            .RequestAborted;

        var workLog = await _workLogRepository
            .GetAsync(id, cancellationToken);

        if (workLog is null)
        {
            return Results.NotFound(WorkLogErrors.WorkLogNotFound(id));
        }

        return await next(efiContext);
    }
}
