using Microsoft.AspNetCore.Mvc.RazorPages;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Web.Pages.Orders
{
    public class BaseOrderPageModel : BasePageModel
    {
        protected readonly IRepository<Order> _orderRepository;

        public BaseOrderPageModel(IRepository<Order> repository)
        {
            _orderRepository = repository;
        }
    }
}
