using Dapper;
using DDD_CQRS.Application.Users.GetByEmail;
using DDD_CQRS.Application.Users.GetById;
using DDD_CQRS.Domain.Users;
using DDD_CQRS.Infrastructure.Data;
using DDD_CQRS.Infrastructure.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DDD_CQRS.Infrastructure.Repositories;

internal sealed class UserRepository : IUserRepository
{
    private readonly ApplicationWriteDbContext _dbContext;
    private readonly DbConnectionFactory _dbConnectionFactory;
    public UserRepository(ApplicationWriteDbContext dbContext, DbConnectionFactory dbConnectionFactory)
    {
        _dbContext = dbContext;
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<object?> GetDetailByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        using IDbConnection connection = _dbConnectionFactory.CreateOpenConnection();

        const string sql =
            """
            SELECT 
                u.Id, 
                u.Email, 
                u.Name, 
                u.HasPublicProfile,
                COUNT(f.UserId) AS NumberOfFollowers
            FROM Users u
            LEFT JOIN Followers f ON f.FollowedId = u.Id
            WHERE u.Id = @Id
            GROUP BY u.Id, u.Email, u.Name, u.HasPublicProfile;
            
            """;

        return await connection.QueryFirstOrDefaultAsync<UserDetailResponse>(
            sql,
            new { Id = id });
    }

    public async Task<bool> IsEmailUniqueAsync(Email email)
    {
        return !await _dbContext.Users.AnyAsync(u => u.Email == email);
    }

    public void Insert(User user)
    {
        _dbContext.Users.Add(user);
    }
}
