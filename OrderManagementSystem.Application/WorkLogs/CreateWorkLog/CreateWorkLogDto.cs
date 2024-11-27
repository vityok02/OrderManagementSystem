using Domain;

namespace Application.WorkLogs.CreateWorkLog;

public record CreateWorkLogDto(
    string CustomerName,
    string? Contacts,
    int WorkTypeId,
    int Amount,
    decimal UnitPrice);
