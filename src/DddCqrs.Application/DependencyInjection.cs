using System.Reflection;
using DddCqrs.Application.Abstractions.Behaviors;
using DddCqrs.Domain.Followers;
using FluentValidation;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace DddCqrs.Application;

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
