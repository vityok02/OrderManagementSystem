using Application.Abstract.Commands;
using Domain.WorkLogs;

namespace Application.WorkLogs.CreateWorkLog;

public record CreateWorkLogCommand(CreateWorkLogDto WorkLogDto) : ICommand<WorkLog>;
