using DddCqrs.SharedKernel;

namespace DddCqrs.Domain.Users;

public static class EmailErrors
{
    public static readonly Error IsNullOrWhiteSpace = Error.Validation("Email.IsNullOrWhiteSpace", "Email is null or white space !");

    public static readonly Error InvalidFormat = Error.Validation(
        "Email.InvalidFormat", "Email format is invalid");
}
