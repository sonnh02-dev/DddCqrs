using DDD_CQRS.SharedKernel;

namespace DDD_CQRS.Infrastructure;

internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime GetUtcNow()
    {
        return DateTime.UtcNow;
    }
}
