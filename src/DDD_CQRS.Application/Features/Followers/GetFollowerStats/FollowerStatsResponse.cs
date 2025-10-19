namespace DDD_CQRS.Application.Features.Followers.GetFollowerStats;

public sealed record FollowerStatsResponse(
      Guid UserId,
    string Email,
    string Name,
    bool HasPublicProfile,
    int FollowerCount,
    int FollowingCount
    );
