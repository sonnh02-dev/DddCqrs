namespace DDD_CQRS.SharedKernel;

public interface IDateTimeProvider
{
    DateTime GetUtcNow();
}
