using Domain.Abstract;
using MediatR;

namespace Application.Abstract.Commands;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
