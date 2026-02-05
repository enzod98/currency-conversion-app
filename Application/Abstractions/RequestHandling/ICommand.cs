using Domain.Abstractions;
using MediatR;

namespace Application.Abstractions.RequestHandling;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
