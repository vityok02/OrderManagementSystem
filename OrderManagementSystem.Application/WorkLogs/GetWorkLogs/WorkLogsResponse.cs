namespace Application.WorkLogs.GetWorkLogs;

public record WorkLogsResponse(IEnumerable<WorkLogDto> WorkLogs);
