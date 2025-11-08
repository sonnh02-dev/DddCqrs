using DddCqrs.SharedKernel;

namespace DddCqrs.Infrastructure;

internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime GetUtcNow()
    {
        return DateTime.UtcNow;
    }
}
