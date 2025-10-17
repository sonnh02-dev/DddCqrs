using DDD_CQRS.Infrastructure.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace DDD_CQRS.Presentation.Extensions;

internal static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using ApplicationWriteDbContext dbContext =
            scope.ServiceProvider.GetRequiredService<ApplicationWriteDbContext>();

        dbContext.Database.Migrate();
    }
}
