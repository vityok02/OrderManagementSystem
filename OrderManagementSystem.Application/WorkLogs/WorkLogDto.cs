using Domain;

namespace Application.WorkLogs;

public record WorkLogDto(
    int Id,
    string CustomerName,
    string? Contacts,
    string WorkType,
    int Amount,
    decimal UnitPrice,
    decimal TotalPrice,
    Status Status);
