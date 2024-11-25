using Application.Abstract.Commands;
using Application.WorkLogs.CreateWorkLog;
using Domain.WorkLogs;

namespace Application.Orders.Commands;

public record CreateWorkLogCommand(CreateWorkLogDto WorkLogDto) : ICommand<WorkLog>;
