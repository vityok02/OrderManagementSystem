using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Web.Pages.OrderTypes
{
    public class ListModel : BaseOrderTypePageModel
    {
        public IEnumerable<WorkType> OrderTypes { get; set; } = Enumerable.Empty<WorkType>();

        public ListModel(IRepository<WorkType> repository) : base(repository) { }

        public async Task OnGetAsync()
        {
            OrderTypes = await _repository.GetAllAsync();
        }

        public IActionResult OnPostSetType(int id)
        {
            Response.Cookies.Append("OrderTypeId", id.ToString());

            return RedirectToPage("/Orders/List");
        }
    }
}
