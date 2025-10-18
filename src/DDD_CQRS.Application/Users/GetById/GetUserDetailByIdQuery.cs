using DDD_CQRS.Application.Abstractions.Caching;

namespace DDD_CQRS.Application.Users.GetById;

public sealed record GetUserDetailByIdQuery(Guid UserId) : ICachedQuery<UserDetailResponse>
{
    public string CacheKey => $"user:by-id:{UserId}";

    public TimeSpan? Expiration => null;
}
