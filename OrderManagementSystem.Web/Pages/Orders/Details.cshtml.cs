using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Web.Pages.Orders;

public class OrderDetailsModel : BaseOrderPageModel
{
    private readonly IRepository<OrderType> _orderTypeRepository;

    public Order? Order { get; set; } = null!;
    public string OrderStatus { get; set; } = string.Empty;

    public OrderDetailsModel(
        IRepository<Order> orderRepository,
        IRepository<OrderType> orderTypeRepository)
        : base(orderRepository)
    {
        _orderTypeRepository = orderTypeRepository;
    }

    public async Task<IActionResult> OnGet(int id)
    {
        if (id is 0)
        {
            return NotFound();
        }

        Order = await _orderRepository.GetAsync(id);

        var orderTypeId = GetOrderTypeId();

        Order!.Type = await _orderTypeRepository.GetAsync(orderTypeId);

        if(Order.Type is null)
        {
            return NotFound();
        }

        return Page();
    }
}
