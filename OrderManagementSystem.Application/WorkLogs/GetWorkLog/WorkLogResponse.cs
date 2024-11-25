using Domain;

namespace Application.WorkLogs.GetWorkLog;

public record WorkLogResponse(Customer Customer, WorkType Type, int Amount, decimal UnitPrice, decimal TotalPrice);
