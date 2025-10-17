using DDD_CQRS.SharedKernel;

namespace DDD_CQRS.Domain.Users;

public sealed record UserCreatedDomainEvent(Guid UserId, Name Name) : IDomainEvent;

