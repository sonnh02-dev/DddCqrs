using System.Data;
using DddCqrs.Application.Abstractions.Data;
using DddCqrs.Application.Abstractions.Notifications;
using DddCqrs.Application.Features.Users;
using DddCqrs.Domain.Followers;
using MediatR;
using DddCqrs.SharedKernel;
using DddCqrs.Application.Features.Users.Queries;
using DddCqrs.Application.Features.Users.Queries.GetById;

namespace DddCqrs.Application.Features.Followers.StartFollowing;

internal class FollowerCreatedDomainEventHandler
    : INotificationHandler<FollowerCreatedDomainEvent>
{
    private readonly ISender _sender;
    private readonly INotificationService _notificationService;

    public FollowerCreatedDomainEventHandler(
        ISender sender,
        INotificationService notificationService)
    {
        _sender = sender;
        _notificationService = notificationService;
    }

    public async Task Handle(FollowerCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery(notification.UserId);

        Result<UserResponse> result = await _sender.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            throw new UserNotFoundException(notification.UserId);
        }

        await _notificationService.SendAsync(
            notification.FollowedId,
            $"{result.Value.Name} started following you!",
            cancellationToken);
    }
}
