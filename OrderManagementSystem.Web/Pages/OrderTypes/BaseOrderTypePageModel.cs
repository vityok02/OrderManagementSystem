using Microsoft.AspNetCore.Mvc.RazorPages;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Web.Pages.OrderTypes;

public class BaseOrderTypePageModel : BasePageModel
{
    protected readonly IRepository<OrderType> _repository;

    public BaseOrderTypePageModel(IRepository<OrderType> repository)
    {
        _repository = repository;
    }
}
