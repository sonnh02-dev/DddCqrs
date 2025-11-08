namespace DddCqrs.Domain.Followers;

public interface IFollowerRepository
{
    Task<bool> IsAlreadyFollowingAsync(
        Guid userId,
        Guid followedId,
        CancellationToken cancellationToken = default);
    Task<object?> GetStatsByUserIdAsync(Guid userId, CancellationToken cancellationToken);

    void Insert(Follower follower);
}
