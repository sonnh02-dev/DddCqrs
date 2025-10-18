namespace DDD_CQRS.Application.Users.GetById;

public sealed record UserDetailResponse
{
    public Guid Id { get; init; }

    public string Email { get; init; }

    public string Name { get; init; }

    public bool HasPublicProfile { get; init; }

    public int NumberOfFollowers { get; init; }
}
