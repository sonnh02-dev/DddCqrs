using DDD_CQRS.Application.Abstractions.Messaging;

namespace DDD_CQRS.Application.Features.Followers.StartFollowing;

public sealed record StartFollowingCommand(Guid UserId, Guid FollowedId) : ICommand;
