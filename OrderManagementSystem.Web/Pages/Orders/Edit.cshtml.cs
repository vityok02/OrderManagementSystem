using Domain.WorkLogs;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Web.Pages.Orders;

public class OrderEditModel : BaseOrderPageModel
{
    public WorkLog Order { get; set; } = null!;
    public OrderEditModel(IRepository<WorkLog> orderRepository) 
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

    public async Task<IActionResult> OnPostAsync(WorkLog order)
    {
        if(order is null)
        {
            return NotFound();
        }

        await _orderRepository.UpdateAsync(order);

        return RedirectToPage("List");
    }
}
