using Application.Abstract.Queries;
using Domain.Abstract;
using Domain.WorkLogs;

namespace Application.WorkLogs.GetWorkLogs;

internal sealed class GetWorkLogsQueryHandler
    : IQueryHandler<GetOrdersQuery, WorkLogsResponse>
{
    private readonly IWorkLogRepository _workLogsRepository;

    public GetWorkLogsQueryHandler(IWorkLogRepository workLogsRepository)
    {
        _workLogsRepository = workLogsRepository;
    }

    public async Task<Result<WorkLogsResponse>> Handle(
        GetOrdersQuery request,
        CancellationToken cancellationToken)
    {
        var orders = await _workLogsRepository.GetAllAsync(cancellationToken);

        var response = new WorkLogsResponse(orders.Select(wl => wl.ToDto()));

        return Result<WorkLogsResponse>.Success(response);
    }
}
