using Application.Abstract.Queries;
using Domain.Abstract;
using Domain.WorkLogs;
using OrderManagementSystem.Models;

namespace Application.WorkLogs.GetWorkLog;

internal class GetWorkLogQueryHandler : IQueryHandler<GetWorkLogQuery, WorkLogResponse>
{
    private readonly IRepository<WorkLog> _workLogRepository;

    public GetWorkLogQueryHandler(IRepository<WorkLog> workLogRepository)
    {
        _workLogRepository = workLogRepository;
    }

    public async Task<Result<WorkLogResponse>> Handle(GetWorkLogQuery request, CancellationToken cancellationToken)
    {
        var workLog = await _workLogRepository.GetAsync(request.Id, cancellationToken);
        if (workLog is null)
        {
            return Result<WorkLogResponse>.Failure("Work log not found.");
        }

        var response = new WorkLogResponse(
            workLog.Customer,
            workLog.WorkType,
            workLog.Amount,
            workLog.UnitPrice,
            workLog.TotalPrice);

        return Result<WorkLogResponse>.Success(response);
    }
}