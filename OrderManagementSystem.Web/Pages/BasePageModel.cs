using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OrderManagementSystem.Web.Pages;

public class BasePageModel : PageModel
{
    protected int GetOrderTypeId()
    {
        if (Request.Cookies.TryGetValue("OrderTypeId", out var strOrderTypeId))
        {
            if (int.TryParse(strOrderTypeId, out int orderTypeId))
            {
                return orderTypeId;
            }
        }
        return 0;
    }
}
