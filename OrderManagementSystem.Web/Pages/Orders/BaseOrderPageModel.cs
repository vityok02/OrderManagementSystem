using Domain.WorkLogs;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Web.Pages.Orders
{
    public class BaseOrderPageModel : BasePageModel
    {
        protected readonly IRepository<WorkLog> _orderRepository;

        public BaseOrderPageModel(IRepository<WorkLog> repository)
        {
            _orderRepository = repository;
        }
    }
}
