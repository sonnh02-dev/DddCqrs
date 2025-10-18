namespace DDD_CQRS.Domain.Users;

public interface IUserRepository
{
    Task<object?> GetDetailByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken);

    Task<bool> IsEmailUniqueAsync(Email email);

    void Insert(User user);
}
