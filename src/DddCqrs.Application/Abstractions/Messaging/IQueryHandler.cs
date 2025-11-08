using MediatR;
using DddCqrs.SharedKernel;

namespace DddCqrs.Application.Abstractions.Messaging;

/// <summary>
/// TResponse là kiểu dữ liệu thuần (UserResponse, ProductResponse, ...).
/// Không được truyền Result<T> vào TResponse để tránh Result<Result<T>>.
/// </summary>
public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IRequest<Result<TResponse>>
    where TResponse : notnull
{ }

