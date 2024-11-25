using Application.Abstract.Commands;
using Domain.Abstract;
using Domain.WorkLogs;
using OrderManagementSystem.Models;

namespace Application.Orders.Commands;

internal class CreateWorkLogCommandHandler : ICommandHandler<CreateWorkLogCommand, WorkLog>
{
    private readonly IRepository<WorkLog> _workLogRepository;

    public CreateWorkLogCommandHandler(IRepository<WorkLog> workLogRepository)
    {
        _workLogRepository = workLogRepository;
    }

    public async Task<Result<WorkLog>> Handle(CreateWorkLogCommand request, CancellationToken cancellationToken)
    {
        var dto = request.WorkLogDto;

        var workLog = new WorkLog(
            dto.Customer, 
            dto.WorkType, 
            dto.Amount, 
            dto.UnitPrice);

        await _workLogRepository.AddAsync(workLog, cancellationToken);

        return Result<WorkLog>.Success(workLog);
    }
}
