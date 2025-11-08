using DddCqrs.Application.Abstractions.Messaging;

namespace DddCqrs.Application.Features.Followers.GetFollowerStats;

public sealed record GetFollowerStatsQuery(Guid UserId) : IQuery<FollowerStatsResponse>;
