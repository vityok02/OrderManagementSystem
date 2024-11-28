namespace Application.WorkLogs;

public record WorkLogDto(
    string CustomerName,
    string? Contacts,
    string WorkType,
    int Amount,
    decimal UnitPrice,
    decimal TotalPrice);
