using DddCqrs.Application.Abstractions.Data;
using DddCqrs.Domain.Followers;
using DddCqrs.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace DddCqrs.Infrastructure.Data.DbContexts;

public sealed class ApplicationWriteDbContext : DbContext, IUnitOfWork
{
    public ApplicationWriteDbContext(DbContextOptions<ApplicationWriteDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }


    public DbSet<Follower> Followers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured) // chỉ cấu hình khi chưa có connection từ DI 
        {
            const string conn = "Data Source=.;Initial Catalog=DddCqrsDb;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true";
            optionsBuilder.UseSqlServer(conn, o => o.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null));
        }
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ApplicationWriteDbContext).Assembly,
            WriteConfigurationsFilter);
    }
    private static bool WriteConfigurationsFilter(Type type) =>
        type.FullName?.Contains("Configurations.Write") ?? false;
}
