using DddCqrs.Domain.Users;
using DddCqrs.SharedKernel;

namespace DddCqrs.Domain.Followers;

public interface IFollowerService
{
    Task<Result> StartFollowingAsync(
        User user,
        User followed,
        CancellationToken cancellationToken);
}
