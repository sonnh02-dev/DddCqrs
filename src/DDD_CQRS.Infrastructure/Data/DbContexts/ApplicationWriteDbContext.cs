using DDD_CQRS.Application.Abstractions.Data;
using DDD_CQRS.Domain.Followers;
using DDD_CQRS.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace DDD_CQRS.Infrastructure.Data.DbContexts;

public sealed class ApplicationWriteDbContext : DbContext, IUnitOfWork
{
    public ApplicationWriteDbContext(DbContextOptions<ApplicationWriteDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }


    public DbSet<Follower> Followers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ApplicationWriteDbContext).Assembly,
            WriteConfigurationsFilter);
    }
    private static bool WriteConfigurationsFilter(Type type) =>
        type.FullName?.Contains("Configurations.Write") ?? false;
}
