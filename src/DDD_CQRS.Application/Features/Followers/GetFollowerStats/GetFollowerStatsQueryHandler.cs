using DDD_CQRS.Application.Abstractions.Data;
using Dapper;
using MediatR;
using System.Data;
using DDD_CQRS.Domain.Followers;
using DDD_CQRS.Application.Abstractions.Messaging;
using DDD_CQRS.SharedKernel;
using DDD_CQRS.Domain.Users;

namespace DDD_CQRS.Application.Features.Followers.GetFollowerStats;

public sealed class GetFollowerStatsQueryHandler
    : IQueryHandler<GetFollowerStatsQuery, FollowerStatsResponse?>
{
    private readonly IFollowerRepository _followerRepository;

    public GetFollowerStatsQueryHandler(IFollowerRepository followerRepository)
    {
        _followerRepository = followerRepository;
    }

    public async Task<Result<FollowerStatsResponse?>> Handle(
        GetFollowerStatsQuery query,
        CancellationToken cancellationToken)
    {
         

        var followerStats = await _followerRepository.GetStatsByUserIdAsync(
           query.UserId,
           cancellationToken);

        return followerStats!=null? 
            (FollowerStatsResponse)followerStats 
            : Result.Failure<FollowerStatsResponse>(UserErrors.NotFound(query.UserId));
    }
}
