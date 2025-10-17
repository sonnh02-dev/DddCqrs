namespace DDD_CQRS.Application.Abstractions.Data;

public interface IUnitOfWork
{

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
