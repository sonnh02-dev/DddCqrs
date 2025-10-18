using AutoMapper;
using AutoMapper.QueryableExtensions;
using DDD_CQRS.Application.Abstractions.Messaging;
using DDD_CQRS.Application.Users.GetById;
using DDD_CQRS.Domain.Users;
using DDD_CQRS.SharedKernel;

namespace DDD_CQRS.Infrastructure.Queries.Users;

internal sealed class GetUserDetailByIdQueryHandler : IQueryHandler<GetUserDetailByIdQuery, UserDetailResponse>
{
    private readonly IUserRepository _userRepository;
    public GetUserDetailByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<UserDetailResponse>> Handle(GetUserDetailByIdQuery query, CancellationToken cancellationToken)
    {

        var user = await _userRepository.GetDetailByIdAsync(query.UserId,cancellationToken);

        if (user is null)
           return Result.Failure<UserDetailResponse>(UserErrors.NotFound(query.UserId));

        return (UserDetailResponse)user;
    }
}
