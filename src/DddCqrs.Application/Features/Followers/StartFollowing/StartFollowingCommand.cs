using DddCqrs.Application.Abstractions.Messaging;

namespace DddCqrs.Application.Features.Followers.StartFollowing;

public sealed record StartFollowingCommand(Guid UserId, Guid FollowedId) : ICommand;
