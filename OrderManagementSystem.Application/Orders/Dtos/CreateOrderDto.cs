using OrderManagementSystem.Models;

namespace Application.Orders.Dtos;

public record CreateOrderDto()
{
    Order Order = new();
}
