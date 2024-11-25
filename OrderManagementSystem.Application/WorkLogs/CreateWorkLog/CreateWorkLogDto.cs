using Domain;

namespace Application.WorkLogs.CreateWorkLog;

public record CreateWorkLogDto(
    Customer Customer,
    WorkType WorkType,
    int Amount,
    decimal UnitPrice);
