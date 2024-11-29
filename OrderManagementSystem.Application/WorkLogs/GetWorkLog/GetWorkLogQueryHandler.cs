using Application.Abstract.Queries;
using Domain.Abstract;
using Domain.WorkLogs;

namespace Application.WorkLogs.GetWorkLog;

internal class GetWorkLogQueryHandler : IQueryHandler<GetWorkLogQuery, WorkLogDto>
{
    private readonly IWorkLogRepository _workLogRepository;

    public GetWorkLogQueryHandler(IWorkLogRepository workLogRepository)
    {
        _workLogRepository = workLogRepository;
    }

    public async Task<Result<WorkLogDto>> Handle(GetWorkLogQuery request, CancellationToken cancellationToken)
    {
        var workLog = await _workLogRepository.GetAsync(request.Id, cancellationToken);

        var response = new WorkLogDto(
            workLog!.Id,
            workLog.Customer.Name,
            workLog.Customer.PhoneNumber,
            workLog.WorkType.Name,
            workLog.Amount,
            workLog.UnitPrice,
            workLog.TotalPrice,
            workLog.Status);

        return Result<WorkLogDto>.Success(response);
    }
}