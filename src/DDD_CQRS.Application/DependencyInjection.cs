using System.Reflection;
using DDD_CQRS.Application.Abstractions.Behaviors;
using DDD_CQRS.Domain.Followers;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DDD_CQRS.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            //config.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
            //config.AddOpenBehavior(typeof(QueryCachingPipelineBehavior<,>));

        });

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}
