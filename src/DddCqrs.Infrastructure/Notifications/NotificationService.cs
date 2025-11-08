using DddCqrs.Application.Abstractions.Notifications;

namespace DddCqrs.Infrastructure.Notifications;

internal sealed class NotificationService : INotificationService
{
    public Task SendAsync(Guid userId, string message, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}
