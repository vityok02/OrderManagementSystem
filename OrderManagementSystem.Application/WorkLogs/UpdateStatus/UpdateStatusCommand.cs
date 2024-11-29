using Application.Abstract.Commands;
using Domain;
using Domain.Abstract;
using Domain.WorkLogs;

namespace Application.WorkLogs.UpdateStatus;

public record UpdateStatusCommand(int Id, int Status) : ICommand;

internal class UpdateStatusCommandHandler
    : ICommandHandler<UpdateStatusCommand>
{
    private readonly IWorkLogRepository _workLogRepository;

    public UpdateStatusCommandHandler(IWorkLogRepository workLogRepository)
    {
        _workLogRepository = workLogRepository;
    }

    public async Task<Result> Handle(UpdateStatusCommand request, CancellationToken cancellationToken)
    {
        if (!Enum.IsDefined(typeof(Status), request.Status))
        {
            return Result.Failure(WorkLogErrors.InvalidStatus);
        }

        var workLog = await _workLogRepository.GetAsync(request.Id, cancellationToken);

        workLog!.SetStatus((Status)request.Status);

        _workLogRepository.Update(workLog);
        await _workLogRepository.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
