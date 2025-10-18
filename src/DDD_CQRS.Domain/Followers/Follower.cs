using DDD_CQRS.Domain.Users;
using DDD_CQRS.SharedKernel;

namespace DDD_CQRS.Domain.Followers;

public sealed class Follower : Entity
{
    public Follower(Guid userId, Guid followedId, DateTime createdOnUtc)
    {
        UserId = userId;
        FollowedId = followedId;
        CreatedOnUtc = createdOnUtc;
    }

    private Follower()
    {
    }

    public Guid UserId { get; private set; }// the user who is following

    public Guid FollowedId { get; private set; }// the user being followed

    public DateTime CreatedOnUtc { get; private set; }

    public static Follower Create(Guid userId, Guid followedId, DateTime createdOnUtc)
    {
        var follower = new Follower(userId, followedId, createdOnUtc);

        follower.Raise(new FollowerCreatedDomainEvent(userId, followedId));

        return follower;
    }

}
