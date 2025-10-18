using FluentValidation;

namespace DDD_CQRS.Application.Users.GetByEmail;

public sealed class GetUserByEmailQueryValidator : AbstractValidator<GetUserByEmailQuery>
{
    public GetUserByEmailQueryValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email is not valid.")
            .MaximumLength(255).WithMessage("Email must not exceed 255 characters.");
    }
}
