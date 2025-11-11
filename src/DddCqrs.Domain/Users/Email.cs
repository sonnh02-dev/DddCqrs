using DddCqrs.SharedKernel;
using System.Text.RegularExpressions;

namespace DddCqrs.Domain.Users;

public sealed record Email //Value-based equality
{
    private Email(string value) => Value = value;

    public string Value { get; }

  

    public static Result<Email> Create(string? email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return Result.Failure<Email>(EmailErrors.IsNullOrWhiteSpace);
        }

        if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        {
            return Result.Failure<Email>(EmailErrors.InvalidFormat);
        }

        return new Email(email);
    }
}
