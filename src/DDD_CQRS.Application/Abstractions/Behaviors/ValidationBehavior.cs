using FluentValidation;
using MediatR;
using DDD_CQRS.SharedKernel;

namespace DDD_CQRS.Application.Abstractions.Behaviors;



public class ValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);

        var failures = (await Task.WhenAll(
            _validators.Select(v => v.ValidateAsync(context, cancellationToken))
        ))
        .SelectMany(r => r.Errors)
        .Select(f => Error.Validation(f.PropertyName, f.ErrorMessage))
        .ToList();

        if (failures.Count != 0)
        {
            // Nếu TResponse = Result<T>, phải tạo failure đúng kiểu
            Type resultType = typeof(TResponse);

            if (resultType.IsGenericType && resultType.GetGenericTypeDefinition() == typeof(Result<>))
            {
                Type innerType = resultType.GetGenericArguments()[0];

                System.Reflection.MethodInfo method = typeof(Result)
                    .GetMethod(nameof(Result.Failure))!
                    .MakeGenericMethod(innerType);

                return (TResponse)method.Invoke(null, new object[] { failures.First() })!;
            }

            return (TResponse)(object)Result.Failure(failures.First());
        }

        return await next();
    }
}
