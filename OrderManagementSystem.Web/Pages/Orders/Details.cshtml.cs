using Domain.WorkLogs;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Web.Pages.Orders;

public class OrderDetailsModel : BaseOrderPageModel
{
    private readonly IRepository<WorkType> _orderTypeRepository;

    public WorkLog? Order { get; set; } = null!;
    public string OrderStatus { get; set; } = string.Empty;

    public OrderDetailsModel(
        IRepository<WorkLog> orderRepository,
        IRepository<WorkType> orderTypeRepository)
        : base(orderRepository)
    {
        _orderTypeRepository = orderTypeRepository;
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        if (id is 0)
        {
            return NotFound();
        }

        Order = await _orderRepository.GetAsync(id);

        var orderTypeId = GetOrderTypeId();

        Order!.Type = await _orderTypeRepository.GetAsync(orderTypeId);

        if(Order.WorkType is null)
        {
            return NotFound();
        }

        return Page();
    }
}
