using DDD_CQRS.SharedKernel;

namespace DDD_CQRS.Domain.Users;

public sealed record Name
{
    public Name(string? value)
    {
        Ensure.NotNullOrEmpty(value);

        Value = value;
    }

    public string Value { get; }
}
