using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Web.Pages.OrderTypes;

public class AddModel : BaseOrderTypePageModel
{
    public WorkType OrderType { get; set; } = null!;
    public AddModel(IRepository<WorkType> repository) : base(repository) {}

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync(WorkType orderType) 
    {
        await _repository.AddAsync(orderType);

        return RedirectToPage("List");
    }
}
