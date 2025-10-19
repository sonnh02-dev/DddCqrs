namespace DDD_CQRS.Domain.Users;

public interface IUserRepository
{
    Task<T?> GetByIdAsync<T>(Guid id, CancellationToken cancellationToken = default) where T : class;
    Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken);

    Task<bool> IsEmailUniqueAsync(Email email);

    void Insert(User user);
}
