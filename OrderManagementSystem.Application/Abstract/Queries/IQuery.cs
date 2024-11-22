using MediatR;
using Domain.Abstract;

namespace Application.Abstract.Queries;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
