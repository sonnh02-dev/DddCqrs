using DDD_CQRS.Application.Abstractions.Messaging;

namespace DDD_CQRS.Application.Features.Users.Commands.Create;

public sealed record CreateUserCommand(string Email, string Name, bool HasPublicProfile)
    : ICommand<Guid>;
