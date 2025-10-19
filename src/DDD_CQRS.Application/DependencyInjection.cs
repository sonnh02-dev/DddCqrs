using System.Reflection;
using DDD_CQRS.Application.Abstractions.Behaviors;
using DDD_CQRS.Domain.Followers;
using FluentValidation;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace DDD_CQRS.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)

    {
        services.AddAutoMapper(typeof(DependencyInjection).Assembly); 
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

            config.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
            config.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
            config.AddOpenBehavior(typeof(QueryCachingPipelineBehavior<,>));

        });
        services.AddScoped<IFollowerService, FollowerService>();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}
