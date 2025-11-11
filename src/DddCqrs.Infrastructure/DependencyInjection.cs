using DddCqrs.Application.Abstractions.Caching;
using DddCqrs.Application.Abstractions.Data;
using DddCqrs.Application.Abstractions.Notifications;
using DddCqrs.Domain.Followers;
using DddCqrs.Domain.Users;
using DddCqrs.Infrastructure.Caching;
using DddCqrs.Infrastructure.Data;
using DddCqrs.Infrastructure.Data.DbContexts;
using DddCqrs.Infrastructure.Notifications;
using DddCqrs.Infrastructure.Repositories;
using DddCqrs.SharedKernel;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace DddCqrs.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddMediatR(config =>
            config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        string? mainConnectString = configuration.GetConnectionString("MyDatabase");
        var defaultConnectString = configuration.GetConnectionString("Default");
        Ensure.NotNullOrEmpty(mainConnectString);
        try
        {
            using var conn = new SqlConnection(mainConnectString);
            conn.Open();
        }
        catch
        {
            mainConnectString = defaultConnectString;
        }
        services.AddTransient(_ => new DbConnectionFactory(mainConnectString));

        services.AddDbContext<ApplicationWriteDbContext>((sp, options) =>
        {
            options.UseSqlServer(
                mainConnectString,
                sqlOptions => sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null)
            );
        });




        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationWriteDbContext>());

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IFollowerRepository, FollowerRepository>();

        services.AddTransient<INotificationService, NotificationService>();

        services.AddMemoryCache();

        services.AddSingleton<ICacheService, CacheService>();



    }
}
