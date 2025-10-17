using DDD_CQRS.SharedKernel;

namespace DDD_CQRS.Domain.Followers;

public sealed record FollowerCreatedDomainEvent(Guid UserId, Guid FollowedId) : IDomainEvent;
