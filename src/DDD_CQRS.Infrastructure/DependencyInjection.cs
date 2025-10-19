using DDD_CQRS.Application.Abstractions.Caching;
using DDD_CQRS.Application.Abstractions.Data;
using DDD_CQRS.Application.Abstractions.Notifications;
using DDD_CQRS.Domain.Followers;
using DDD_CQRS.Domain.Users;
using DDD_CQRS.Infrastructure.Caching;
using DDD_CQRS.Infrastructure.Data;
using DDD_CQRS.Infrastructure.Data.DbContexts;
using DDD_CQRS.Infrastructure.Notifications;
using DDD_CQRS.Infrastructure.Repositories;
using DDD_CQRS.SharedKernel;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace DDD_CQRS.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddMediatR(config =>
            config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        string? connectionString = configuration.GetConnectionString("MyDatabase");
        Ensure.NotNullOrEmpty(connectionString);

        services.AddTransient(_ => new DbConnectionFactory(connectionString));
        services.AddDbContext<ApplicationWriteDbContext>(
          (sp, options) => options
        .UseSqlServer(connectionString));



        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationWriteDbContext>());

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IFollowerRepository, FollowerRepository>();

        services.AddTransient<INotificationService, NotificationService>();

        services.AddMemoryCache();

        services.AddSingleton<ICacheService, CacheService>();



    }
}
