using DddCqrs.Application.Abstractions.Messaging;

namespace DddCqrs.Application.Features.Users.Commands.Create;

public sealed record CreateUserCommand(string Email, string Name, bool HasPublicProfile)
    : ICommand<Guid>;
