using DddCqrs.Application.Abstractions.Caching;

namespace DddCqrs.Application.Features.Users.Queries.GetByEmail;

public sealed record GetUserByEmailQuery(string Email) : ICachedQuery<UserResponse>
{
    public string CacheKey => $"user:by-email:{Email}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(10);
}
