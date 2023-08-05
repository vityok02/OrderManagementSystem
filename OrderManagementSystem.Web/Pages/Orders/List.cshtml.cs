using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Web.Pages.Orders;

public class OrdersListModel : BaseOrderPageModel
{
    private readonly IRepository<OrderType> _orderTypeRepository;
    private readonly IConfiguration _configuration;

    public PaginatedList<Order> Orders { get; private set; } = default!;
    public IEnumerable<OrderType> OrderTypes { get; private set;} = Enumerable.Empty<OrderType>();
    public string OrderStatus { get; private set; } = string.Empty;
    public int? ActiveOrderTypeId { get; private set; }
    public int TotalPages => PaginatedList<Order>.TotalPages;
    public int PageSize => _configuration.GetValue("PageSize", 4);
    public int? PageIndex { get; private set; }

    public OrdersListModel(
        IRepository<Order> repository,
        IRepository<OrderType> orderTypeRepository,
        IConfiguration configuration)
        : base(repository)
    {
        _orderTypeRepository = orderTypeRepository;
        _configuration = configuration;
    }

    public async Task OnGetAsync(int? id = null!, int? pageIndex = null!)
    {

        ActiveOrderTypeId = id;
        Response.Cookies.Append("OrderTypeId", id.ToString()!);

        if(pageIndex is null)
        {
            PageIndex = 1;
        }
        else
        {
            PageIndex = pageIndex;
        }

        var orders = await GetOrdersAsync(id);
        OrderTypes = await GetOrderTypesAsync();

        var pageSize = _configuration.GetValue("PageSize", 4);

        Orders = PaginatedList<Order>.Create(orders, pageIndex ?? 1, pageSize);
    }

    public async Task<IActionResult> OnPostConfirmOrderAsync(int id, int? otId = null!, int? pageIndex = null!)
    {
        var order = await _orderRepository.GetAsync(id);

        if(order is null)
        {
            return NotFound();
        }

        order.IsCompleted = true;
        await _orderRepository.UpdateAsync(order);

        //return await RedirectToThisPage(otId, pageIndex);
        return RedirectToPage("List", new {pageIndex = pageIndex , id = otId});
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id, int? otId = null!, int? pageIndex = null!)
    {
        var order = await _orderRepository.GetAsync(id);

        if(order is null )
        {
            return NotFound();
        }

        await _orderRepository.DeleteAsync(order);

        OrderTypes = await GetOrderTypesAsync();

        //return await RedirectToThisPage(otId, pageIndex);
        return RedirectToPage("List");
    }

    private async Task<IEnumerable<OrderType>> GetOrderTypesAsync()
    {
        return await _orderTypeRepository.GetAllAsync();
    }


    private async Task<PageResult> RedirectToThisPage(int? id = null!, int? pageIndex = null!)
    {
        var orders = await GetOrdersAsync(id);
        Orders = PaginatedList<Order>.Create(orders, pageIndex ?? 1, PageSize);

        OrderTypes = await GetOrderTypesAsync();

        return Page();
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