using DDD_CQRS.Application.Abstractions.Messaging;

namespace DDD_CQRS.Application.Features.Followers.GetFollowerStats;

public sealed record GetFollowerStatsQuery(Guid UserId) : IQuery<FollowerStatsResponse>;
