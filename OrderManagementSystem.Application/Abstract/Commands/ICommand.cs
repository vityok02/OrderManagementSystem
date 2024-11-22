using MediatR;
using Domain.Abstract;

namespace Application.Abstract.Commands;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
