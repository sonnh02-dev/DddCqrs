using AutoMapper;
using Dapper;
using DddCqrs.Application.Abstractions.Messaging;
using DddCqrs.Domain.Users;
using DddCqrs.SharedKernel;
using System.Data;

namespace DddCqrs.Application.Features.Users.Queries.GetByEmail;

internal sealed class GetUserByEmailQueryHandler : IQueryHandler<GetUserByEmailQuery, UserResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserByEmailQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<UserResponse>> Handle(GetUserByEmailQuery query, CancellationToken cancellationToken)
    {
        var createResult = Email.Create(query.Email);

        if (createResult.IsFailure)
            return Result.Failure<UserResponse>(createResult.Error);

        var user = await _userRepository.GetByEmailAsync(createResult.Value, cancellationToken);

        if (user is null)
            return Result.Failure<UserResponse>(UserErrors.NotFoundByEmail(query.Email));

        return _mapper.Map<UserResponse>(user);
    }
}
