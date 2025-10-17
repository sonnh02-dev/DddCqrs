using DDD_CQRS.Application.Abstractions.Notifications;

namespace DDD_CQRS.Infrastructure.Notifications;

internal sealed class NotificationService : INotificationService
{
    public Task SendAsync(Guid userId, string message, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}
