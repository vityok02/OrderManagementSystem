using Application.Abstract.Queries;

namespace Application.WorkLogs.GetWorkLogs;

public record GetOrdersQuery() : IQuery<OrdersResponse>;
