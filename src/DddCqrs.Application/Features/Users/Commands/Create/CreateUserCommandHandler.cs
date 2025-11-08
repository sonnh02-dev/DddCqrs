using DddCqrs.Application.Abstractions.Data;
using DddCqrs.Application.Abstractions.Messaging;
using DddCqrs.Domain.Users;
using DddCqrs.SharedKernel;

namespace DddCqrs.Application.Features.Users.Commands.Create;

public sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        Result<Email> emailResult = Email.Create(command.Email);
        if (emailResult.IsFailure)
        {
            return Result.Failure<Guid>(emailResult.Error);
        }

        Email email = emailResult.Value;
        if (!await _userRepository.IsEmailUniqueAsync(email))
        {
            return Result.Failure<Guid>(UserErrors.EmailNotUnique);
        }

        var name = new Name(command.Name);
        var user = User.Create(email, name, command.HasPublicProfile);

        _userRepository.Insert(user);


        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
