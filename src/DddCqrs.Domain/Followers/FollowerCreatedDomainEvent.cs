using DddCqrs.SharedKernel;

namespace DddCqrs.Domain.Followers;

public sealed record FollowerCreatedDomainEvent(Guid UserId, Guid FollowedId) : IDomainEvent;
