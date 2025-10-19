using AutoMapper;
using AutoMapper.QueryableExtensions;
using DDD_CQRS.Application.Abstractions.Messaging;
using DDD_CQRS.Domain.Users;
using DDD_CQRS.SharedKernel;

namespace DDD_CQRS.Application.Features.Users.Queries.GetById;

internal sealed class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserResponse>
{
    private readonly IUserRepository _userRepository;
    public GetUserByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<UserResponse>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {

        var user = await _userRepository.GetByIdAsync<UserResponse>(query.UserId,cancellationToken);

        if (user is null)
           return Result.Failure<UserResponse>(UserErrors.NotFound(query.UserId));

        return user;
    }
}
