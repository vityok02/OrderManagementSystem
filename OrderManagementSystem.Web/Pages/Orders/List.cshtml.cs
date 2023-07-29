using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Web.Pages.Orders;

public class OrdersListModel : BaseOrderPageModel
{
    private readonly IRepository<OrderType> _orderTypeRepository;

    public IEnumerable<Order> Orders { get; set; } = Enumerable.Empty<Order>();
    public IEnumerable<OrderType> OrderTypes { get; set;} = Enumerable.Empty<OrderType>();
    public string OrderStatus { get; set; } = string.Empty;

    public OrdersListModel(
        IRepository<Order> repository,
        IRepository<OrderType> orderTypeRepository)
        : base(repository)
    {
        _orderTypeRepository = orderTypeRepository;
    }

    public async Task OnGetAsync()
    {
        Orders = await GetOrders();
        OrderTypes = await _orderTypeRepository.GetAllAsync();

        foreach (var order in Orders)
        {
            order.SetStatus();
        }
    }

    public async Task<IActionResult> OnPostConfirmOrder(int id)
    {
        var order = await _orderRepository.GetAsync(id);

        if(order is null)
        {
            return NotFound();
        }

        order.IsCompleted = true;
        await _orderRepository.UpdateAsync(order);

        Orders = await GetOrders();

        return Page();
    }

    public async Task<IActionResult> OnPostDelete(int id)
    {
        var order = await _orderRepository.GetAsync(id);

        if(order is null )
        {
            return NotFound();
        }

        await _orderRepository.DeleteAsync(order);

        Orders = await GetOrders();

        return Page();
    }

    private async Task<IEnumerable<Order>> GetOrders()
    {
        var orderTypeId = GetOrderTypeId();

        var orders = await _orderRepository.GetAllAsync(o => o.TypeId == orderTypeId);
        orders = orders.OrderBy(o => o.IsCompleted);

        foreach (var order in Orders)
        {
            order.SetStatus();
        }

        return orders;
    }
}