using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Web.Pages.Orders
{
    public class CreateOrderModel : BaseOrderPageModel
    {
        public Order Order { get; set; } = null!;
        private readonly IRepository<OrderType> _orderTypeRepository;

        public CreateOrderModel(
            IRepository<Order> repository, 
            IRepository<OrderType> orderTypeRepository) 
            : base(repository)
        {
            _orderTypeRepository = orderTypeRepository;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var orderTypeId = GetOrderTypeId();

            var orderType = await _orderTypeRepository.GetAsync(orderTypeId);

            ValidateOrderType(orderType!);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Order order) 
        {
            var orderTypeId = GetOrderTypeId();

            var orderType = await _orderTypeRepository.GetAsync(orderTypeId);

            ValidateOrderType(orderType!);

            Order = order;
            Order.SetPrice();
            Order.Type = orderType;

            await _orderRepository.AddAsync(Order);

            return RedirectToPage("List");
        }

        private IActionResult ValidateOrderType(OrderType orderType)
        {
            if(orderType is null)
            {
                return NotFound();
            }

            return null!;
        }
    }
}
