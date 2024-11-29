using Application.Abstract.Commands;
using Domain;
using Domain.Abstract;
using Domain.WorkLogs;

namespace Application.WorkLogs.CreateWorkLog;

internal class CreateWorkLogCommandHandler
    : ICommandHandler<CreateWorkLogCommand, WorkLogDto>
{
    private readonly IWorkLogRepository _workLogRepository;
    private readonly IRepository<WorkType> _workTypeRepository;

    public CreateWorkLogCommandHandler(
        IWorkLogRepository workLogRepository,
        IRepository<WorkType> workTypeRepository)
    {
        _workLogRepository = workLogRepository;
        _workTypeRepository = workTypeRepository;
    }

    public async Task<Result<WorkLogDto>> Handle(
        CreateWorkLogCommand request,
        CancellationToken cancellationToken)
    {
        var dto = request.WorkLogDto;

        var workType = await _workTypeRepository
            .GetAsync(dto.WorkTypeId, cancellationToken);

        if (!await _workTypeRepository.AnyAsync(cancellationToken))
        {
            return Result<WorkLogDto>
                .Failure(CreateWorkLogErrors.WorkTypesEmpty);
        }

        if (workType is null)
        {
            return Result<WorkLogDto>
                .Failure(CreateWorkLogErrors.WorkTypeNotFound(dto.WorkTypeId));
        }

        var workLog = new WorkLog(
            new Customer(dto.CustomerName, dto.Contacts),
            workType,
            dto.Amount,
            dto.UnitPrice);

        await _workLogRepository.CreateAsync(workLog, cancellationToken);
        await _workLogRepository.SaveChangesAsync(cancellationToken);

        return Result<WorkLogDto>.Success(workLog.ToDto());
    }
}
