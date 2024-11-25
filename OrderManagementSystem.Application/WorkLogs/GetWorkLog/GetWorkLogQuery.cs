using Application.Abstract.Queries;

namespace Application.WorkLogs.GetWorkLog;

public record GetWorkLogQuery(int Id) : IQuery<WorkLogResponse>;
