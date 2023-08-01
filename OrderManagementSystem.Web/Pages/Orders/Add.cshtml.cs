using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Web.Pages.Orders
{
    public class CreateOrderModel : BaseOrderPageModel
    {
        private readonly IRepository<OrderType> _orderTypeRepository;

        public Order Order { get; private set; } = null!;
        public int OrderTypeId { get; private set; }

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
            OrderTypeId = orderTypeId;

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

            return RedirectToPage("List", new { id = orderTypeId});
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
