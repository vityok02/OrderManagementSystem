using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Web.Pages.Orders;

public class OrdersListModel : BaseOrderPageModel
{
    private readonly IRepository<OrderType> _orderTypeRepository;

    public IEnumerable<Order> Orders { get; set; } = Enumerable.Empty<Order>();
    public IEnumerable<OrderType> OrderTypes { get; set;} = Enumerable.Empty<OrderType>();
    public string OrderStatus { get; set; } = string.Empty;
    public int? ActiveOrderTypeId { get; set; }

    public OrdersListModel(
        IRepository<Order> repository,
        IRepository<OrderType> orderTypeRepository)
        : base(repository)
    {
        _orderTypeRepository = orderTypeRepository;
    }

    public async Task OnGetAsync()
    {
        Orders = await GetOrdersAsync();
        OrderTypes = await GetOrderTypesAsync();
    }

    public async Task<IActionResult> OnPostSelectOrderType(int? id)
    {
        Orders = await GetOrdersAsync(id);
        
        OrderTypes = await GetOrderTypesAsync();

        Response.Cookies.Append("OrderTypeId", id.ToString()!);

        return Page();
    }

    public async Task<IActionResult> OnPostConfirmOrderAsync(int id)
    {
        var order = await _orderRepository.GetAsync(id);

        if(order is null)
        {
            return NotFound();
        }

        order.IsCompleted = true;
        await _orderRepository.UpdateAsync(order);

        int? orderTypeId = GetOrderTypeId();
        if(orderTypeId is not null)
        { 
            Orders = await GetOrdersAsync(orderTypeId);
        }
        else
        {
            OrderTypes = await GetOrderTypesAsync();
        }

        OrderTypes = await GetOrderTypesAsync();

        return Page();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var order = await _orderRepository.GetAsync(id);

        if(order is null )
        {
            return NotFound();
        }

        await _orderRepository.DeleteAsync(order);

        Orders = await GetOrdersAsync();
        OrderTypes = await GetOrderTypesAsync();

        return Page();
    }

    private Task<IEnumerable<OrderType>> GetOrderTypesAsync()
    {
        return _orderTypeRepository.GetAllAsync();
    }

    private async Task<IEnumerable<Order>> GetOrdersAsync(int? id = null!)
    {
        IEnumerable<Order> orders;
        if (id is not null)
        {
            ActiveOrderTypeId = id;

            orders = await _orderRepository.GetAllAsync(o => o.TypeId == id);
        }
        else
        {
            orders = await _orderRepository.GetAllAsync();
        }

        orders = orders.OrderBy(o => o.IsCompleted);

        return orders;
    }
}