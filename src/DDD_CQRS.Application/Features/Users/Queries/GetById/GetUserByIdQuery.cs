using DDD_CQRS.Application.Abstractions.Caching;

namespace DDD_CQRS.Application.Features.Users.Queries.GetById;

public sealed record GetUserByIdQuery(Guid UserId) : ICachedQuery<UserResponse>
{
    public string CacheKey => $"user:by-id:{UserId}";

    public TimeSpan? Expiration => null;
}
