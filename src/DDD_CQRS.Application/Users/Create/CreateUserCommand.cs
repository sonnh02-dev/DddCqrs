using DDD_CQRS.Application.Abstractions.Messaging;

namespace DDD_CQRS.Application.Users.Create;

public sealed record CreateUserCommand(string Email, string Name, bool HasPublicProfile)
    : ICommand<Guid>;
