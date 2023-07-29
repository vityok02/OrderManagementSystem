using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Web.Pages.OrderTypes
{
    public class ListModel : BaseOrderTypePageModel
    {
        public IEnumerable<OrderType> OrderTypes { get; set; } = Enumerable.Empty<OrderType>();

        public ListModel(IRepository<OrderType> repository) : base(repository) { }

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
