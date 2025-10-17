using MediatR;
using DDD_CQRS.SharedKernel;

namespace DDD_CQRS.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
