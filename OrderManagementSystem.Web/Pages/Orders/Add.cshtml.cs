using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Web.Pages.Orders
{
    public class CreateOrderModel : BaseOrderPageModel
    {
        private readonly IRepository<WorkType> _orderTypeRepository;

        public Order Order { get; private set; } = null!;
        public IEnumerable<WorkType> OrderTypes { get; private set; } = Enumerable.Empty<WorkType>();
        public int OrderTypeId { get; private set; }

        public CreateOrderModel(
            IRepository<Order> repository, 
            IRepository<WorkType> orderTypeRepository) 
            : base(repository)
        {
            _orderTypeRepository = orderTypeRepository;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            OrderTypes = await _orderTypeRepository.GetAllAsync();

            OrderTypeId = GetOrderTypeId();

            var orderType = await _orderTypeRepository.GetAsync(OrderTypeId);
            ValidateOrderType(orderType!);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Order order, int orderTypeId) 
        {
            var activeOrderTypeId = GetOrderTypeId();

            if (orderTypeId is 0)
            {
                orderTypeId = activeOrderTypeId;
            }

            var orderType = await _orderTypeRepository.GetAsync(orderTypeId);

            ValidateOrderType(orderType!);

            Order = order;
            Order.Type = orderType;

            await _orderRepository.AddAsync(Order);

            return RedirectToPage("List", new { id = activeOrderTypeId});
        }

        private IActionResult ValidateOrderType(WorkType orderType)
        {
            if(orderType is null)
            {
                return NotFound();
            }

            return null!;
        }
    }
}
