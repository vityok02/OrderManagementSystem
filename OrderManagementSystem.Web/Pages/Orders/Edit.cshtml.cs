using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Web.Pages.Orders;

public class OrderEditModel : BaseOrderPageModel
{
    public Order Order { get; set; } = null!;
    public OrderEditModel(IRepository<Order> orderRepository) 
        : base(orderRepository) { }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var order = await _orderRepository.GetAsync(id);

        if (order is null)
        {
            return NotFound();
        }

        Order = order;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(Order order)
    {
        if(order is null)
        {
            return NotFound();
        }

        await _orderRepository.UpdateAsync(order);

        return RedirectToPage("List");
    }
}
