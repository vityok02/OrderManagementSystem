using Microsoft.AspNetCore.Mvc.RazorPages;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Web.Pages.OrderTypes;

public class BaseOrderTypePageModel : BasePageModel
{
    protected readonly IRepository<WorkType> _repository;

    public BaseOrderTypePageModel(IRepository<WorkType> repository)
    {
        _repository = repository;
    }
}
