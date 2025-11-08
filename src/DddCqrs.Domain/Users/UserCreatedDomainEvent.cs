using DddCqrs.SharedKernel;

namespace DddCqrs.Domain.Users;

public sealed record UserCreatedDomainEvent(Guid UserId, Name Name) : IDomainEvent;

