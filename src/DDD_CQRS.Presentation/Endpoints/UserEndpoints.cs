
using DDD_CQRS.Application.Users.Create;

using MediatR;
using DDD_CQRS.SharedKernel;
using DDD_CQRS.Presentation.Extensions;

namespace DDD_CQRS.Presentation.Endpoints;

internal static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("api/users", async (CreateUserCommand command, ISender sender) =>
        {
            Result<Guid> result = await sender.Send(command);

            return result.IsSuccess ? Results.Ok(result.Value) : result.ToProblemDetails();
        });

       
    }
}
