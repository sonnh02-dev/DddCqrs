namespace DddCqrs.SharedKernel;

public interface IDateTimeProvider
{
    DateTime GetUtcNow();
}
