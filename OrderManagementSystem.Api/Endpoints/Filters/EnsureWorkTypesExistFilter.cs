using Application.WorkLogs.CreateWorkLog;
using Domain;
using Domain.Abstract;

namespace Api.Endpoints.Filters;

public class EnsureWorkTypesExistFilter : IEndpointFilter
{
    private readonly IRepository<WorkType> _workTypeRepository;
    private readonly LinkGenerator _linkGenerator;

    public EnsureWorkTypesExistFilter(IRepository<WorkType> workTypeRepository, LinkGenerator linkGenerator)
    {
        _workTypeRepository = workTypeRepository;
        _linkGenerator = linkGenerator;
    }

    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext efiContext,
        EndpointFilterDelegate next)
    {
        var cancellationToken = efiContext.HttpContext.RequestAborted;

        if (!await _workTypeRepository.AnyAsync(cancellationToken))
        {
            var redirectUrl = _linkGenerator.GetUriByName(efiContext.HttpContext, "CreateWorkType");
            return Results.BadRequest(new
            {
                Message = "WorkTypes are empty. You need to create at least one WorkType before creating a WorkLog.",
                RedirectUrl = redirectUrl
            });
        }

        return await next(efiContext);
    }
}
