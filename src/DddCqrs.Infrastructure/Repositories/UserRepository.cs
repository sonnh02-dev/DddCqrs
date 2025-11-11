using Dapper;
using DddCqrs.Application.Features.Users.Queries;
using DddCqrs.Application.Features.Users.Queries.GetById;
using DddCqrs.Domain.Users;
using DddCqrs.Infrastructure.Data;
using DddCqrs.Infrastructure.Data.DbContexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Runtime.CompilerServices;

namespace DddCqrs.Infrastructure.Repositories;

internal sealed class UserRepository : IUserRepository
{
    private readonly ApplicationWriteDbContext _dbContext;
    public UserRepository(ApplicationWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<T?> GetByIdAsync<T>(Guid id, CancellationToken cancellationToken)
           where T : class
    {
        FormattableString query;

        if (typeof(T) == typeof(User))
        {
            query = $"SELECT * FROM Users WHERE Id = {id}";
        }
        else if (typeof(T) == typeof(UserResponse))
        {
            query = $"""
                SELECT 
                  Id, 
                  Email, 
                  Name, 
                  HasPublicProfile
                FROM Users 
                WHERE Id = {id}
               """;
        }
        else
        {
            throw new NotSupportedException($"Type {typeof(T).Name} is not supported");
        }

        return await _dbContext.Database
            .SqlQuery<T>(query)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);

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
