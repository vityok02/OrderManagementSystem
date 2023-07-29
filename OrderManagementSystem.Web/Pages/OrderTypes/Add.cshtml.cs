using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Web.Pages.OrderTypes;

public class AddModel : BaseOrderTypePageModel
{
    public OrderType OrderType { get; set; } = null!;
    public AddModel(IRepository<OrderType> repository) : base(repository) {}

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync(OrderType orderType) 
    {
        await _repository.AddAsync(orderType);

        return RedirectToPage("List");
    }
}
