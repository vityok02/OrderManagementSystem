using Application.Abstract.Queries;
using Domain.Abstract;
using OrderManagementSystem.Models;

namespace Application.Orders.Queries;

public record GetOrdersQuery() : IQuery<OrdersResponse>;

public record OrdersResponse(IEnumerable<Order> Orders);

internal sealed class GetOrdersQueryHandler
    : IQueryHandler<GetOrdersQuery, OrdersResponse>
{
    private readonly IRepository<Order> _ordersRepository;

    public GetOrdersQueryHandler(IRepository<Order> ordersRepository)
    {
        _ordersRepository = ordersRepository;
    }

    public async Task<Result<OrdersResponse>> Handle(
        GetOrdersQuery request,
        CancellationToken cancellationToken)
    {
        var orders = await _ordersRepository.GetAllAsync(cancellationToken);
        var response = new OrdersResponse(orders);
        return Result<OrdersResponse>.Success(response);
    }
}
