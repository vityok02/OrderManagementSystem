using Application.Abstract.Queries;
using Domain.Abstract;
using Domain.WorkLogs;
using OrderManagementSystem.Models;

namespace Application.WorkLogs.GetWorkLogs;

internal sealed class GetOrdersQueryHandler
    : IQueryHandler<GetOrdersQuery, OrdersResponse>
{
    private readonly IRepository<WorkLog> _ordersRepository;

    public GetOrdersQueryHandler(IRepository<WorkLog> ordersRepository)
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
