using DDD_CQRS.Application.Abstractions.Data;
using DDD_CQRS.Application.Abstractions.Messaging;
using DDD_CQRS.Application.Features.Users.Queries;
using DDD_CQRS.Domain.Followers;
using DDD_CQRS.Domain.Users;
using DDD_CQRS.SharedKernel;

namespace DDD_CQRS.Application.Features.Followers.StartFollowing;

public sealed class StartFollowingCommandHandler : ICommandHandler<StartFollowingCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IFollowerService _followerService;
    private readonly IUnitOfWork _unitOfWork;

    public StartFollowingCommandHandler(
        IUserRepository userRepository,
        IFollowerService followerService,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _followerService = followerService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(StartFollowingCommand command, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync<User>(command.UserId, cancellationToken);
        if (user is null)
        {
            return Result.Failure(UserErrors.NotFound(command.UserId));
        }

        var followed = await _userRepository.GetByIdAsync<User>(command.FollowedId, cancellationToken);
        if (followed is null)
        {
            return Result.Failure(UserErrors.NotFound(command.FollowedId));
        }

        Result result = await _followerService.StartFollowingAsync(
            user,
            followed,
            cancellationToken);

        if (result.IsFailure)
        {
            return result;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
