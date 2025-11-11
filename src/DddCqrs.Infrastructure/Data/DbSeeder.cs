using DddCqrs.Infrastructure.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace DddCqrs.Infrastructure.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(ApplicationWriteDbContext context, IServiceProvider services, CancellationToken ct = default)
    {
        if (await context.Users.AnyAsync(ct))
            return;

        var users = new[]
        {
            new { Id = Guid.NewGuid(), Email = "alice@example.com", Name = "Alice", HasPublicProfile = true },
            new { Id = Guid.NewGuid(), Email = "bob@example.com", Name = "Bob", HasPublicProfile = true },
            new { Id = Guid.NewGuid(), Email = "charlie@example.com", Name = "Charlie", HasPublicProfile = false }
        };

        foreach (var u in users)
        {
            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO Users (Id, Email, Name, HasPublicProfile) VALUES ({0}, {1}, {2}, {3})",
                u.Id, u.Email, u.Name, u.HasPublicProfile);
        }

        var followers = new[]
        {
            new { UserId = users[1].Id, FollowedId = users[0].Id }, 
            new { UserId = users[2].Id, FollowedId = users[0].Id }  
        };

        foreach (var f in followers)
        {
            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO Followers (UserId, FollowedId, CreatedOnUtc) VALUES ({0}, {1}, {2})",
                f.UserId, f.FollowedId, DateTime.UtcNow);
        }

        await context.SaveChangesAsync(ct);
    }
}
