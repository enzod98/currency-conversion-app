using Domain.Abstractions;
using MediatR;

namespace Application.Abstractions.RequestHandling;

public interface IQuery<IResponse> : IRequest<Result<IResponse>>
{
}
