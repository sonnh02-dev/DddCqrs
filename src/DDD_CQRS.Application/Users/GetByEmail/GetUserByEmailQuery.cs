using DDD_CQRS.Application.Abstractions.Caching;

namespace DDD_CQRS.Application.Users.GetByEmail;

public sealed record GetUserByEmailQuery(string Email) : ICachedQuery<UserResponse>
{
    public string CacheKey => $"user:by-email:{Email}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(10);
}
