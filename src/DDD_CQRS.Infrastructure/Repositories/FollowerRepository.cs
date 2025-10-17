using DDD_CQRS.Domain.Followers;
using DDD_CQRS.Infrastructure.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace DDD_CQRS.Infrastructure.Repositories;

internal sealed class FollowerRepository : IFollowerRepository
{
    private readonly ApplicationWriteDbContext _dbContext;

    public FollowerRepository(ApplicationWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> IsAlreadyFollowingAsync(
        Guid userId,
        Guid followedId,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Followers.AnyAsync(f =>
            f.UserId == userId && f.FollowedId == followedId,
            cancellationToken);
    }

    public void Insert(Follower follower)
    {
        _dbContext.Followers.Add(follower);
    }
}
