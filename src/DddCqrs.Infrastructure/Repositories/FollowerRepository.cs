using Dapper;
using DddCqrs.Application.Features.Followers.GetFollowerStats;
using DddCqrs.Domain.Followers;
using DddCqrs.Infrastructure.Data;
using DddCqrs.Infrastructure.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DddCqrs.Infrastructure.Repositories;

internal sealed class FollowerRepository : IFollowerRepository
{
    private readonly ApplicationWriteDbContext _dbContext;
    private readonly DbConnectionFactory _dbConnectionFactory;

    public FollowerRepository(ApplicationWriteDbContext dbContext, DbConnectionFactory dbConnectionFactory)
    {
        _dbContext = dbContext;
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<bool> IsAlreadyFollowingAsync(
        Guid userId,
        Guid followedId,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Followers.AnyAsync(f =>
            f.UserId == userId && f.FollowedId == followedId,
            cancellationToken);
    }
    public async Task<object?> GetStatsByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        using IDbConnection connection = _dbConnectionFactory.CreateOpenConnection();

        const string sql = """
            SELECT 
                u.Id AS UserId,
                u.Email,
                u.Name,
                u.HasPublicProfile,
                COUNT(DISTINCT f1.UserId) AS FollowerCount,
                COUNT(DISTINCT f2.FollowedId) AS FollowingCount
            FROM Users u
            LEFT JOIN Followers f1 ON f1.FollowedId = u.Id      
            LEFT JOIN Followers f2 ON f2.UserId = u.Id          
            WHERE u.Id = @UserId
            GROUP BY u.Id, u.Email, u.Name, u.HasPublicProfile;
            
            """;

        return await connection.QueryFirstOrDefaultAsync<FollowerStatsResponse>(
            sql,
            new { UserId = userId });
    }
    public void Insert(Follower follower)
    {
        _dbContext.Followers.Add(follower);
    }
}
