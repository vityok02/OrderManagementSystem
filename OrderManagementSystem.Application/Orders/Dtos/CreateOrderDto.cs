using Domain.WorkLogs;

namespace Application.Orders.Dtos;

public record CreateOrderDto()
{
    WorkLog Order = new();
}
