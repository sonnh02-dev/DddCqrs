using DDD_CQRS.Domain.Users;
using DDD_CQRS.SharedKernel;

namespace DDD_CQRS.Domain.Followers;

public interface IFollowerService
{
    Task<Result> StartFollowingAsync(
        User user,
        User followed,
        CancellationToken cancellationToken);
}
