using DDD_CQRS.Application.Abstractions.Messaging;

namespace DDD_CQRS.Application.Abstractions.Caching;

public interface ICachedQuery<TResponse> : IQuery<TResponse>, ICachedQuery;

public interface ICachedQuery
{
    string CacheKey { get; }

    TimeSpan? Expiration { get; }
}

