using Domain.WorkLogs;

namespace Application.WorkLogs.GetWorkLogs;

public record OrdersResponse(IEnumerable<WorkLog> Orders);
