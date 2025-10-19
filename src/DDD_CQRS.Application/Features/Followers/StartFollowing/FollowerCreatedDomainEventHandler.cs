using System.Data;
using DDD_CQRS.Application.Abstractions.Data;
using DDD_CQRS.Application.Abstractions.Notifications;
using DDD_CQRS.Application.Features.Users;
using DDD_CQRS.Domain.Followers;
using MediatR;
using DDD_CQRS.SharedKernel;
using DDD_CQRS.Application.Features.Users.Queries;
using DDD_CQRS.Application.Features.Users.Queries.GetById;

namespace DDD_CQRS.Application.Features.Followers.StartFollowing;

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
