using MediatR;
using DddCqrs.SharedKernel;

namespace DddCqrs.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
