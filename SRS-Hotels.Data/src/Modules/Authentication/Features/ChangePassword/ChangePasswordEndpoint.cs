using Carter;
using MediatR;
using Mapster;
using System.Security.Claims;

namespace SRS_Hotels.Data.src.Modules.Authentication.Features.ChangePassword
{
  
    public class ChangePasswordRequest
    {
        public string CurrentPassword { get; set; } = default!;
        public string NewPassword { get; set; } = default!;
    }

    public class ChangePasswordResponse
    {
        public bool Successful { get; set; }
        public string Message { get; set; } = default!;
    }

    public class ChangePasswordEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/api/auth/change-password", async (
                ChangePasswordRequest request,
                HttpContext httpContext,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var userIdClaim =
                    httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                    ?? httpContext.User.FindFirst("sub")?.Value;

                if (userIdClaim is null)
                    return Results.Unauthorized();

                var userId = Guid.Parse(userIdClaim);

                var command = new ChangePasswordCommand(
                    userId,
                    request.CurrentPassword,
                    request.NewPassword
                );

                var result = await sender.Send(command, cancellationToken);

                var response = result.Adapt<ChangePasswordResponse>();

                return result.Successful
                    ? Results.Ok(response)
                    : Results.BadRequest(response);
            })
            .WithName("ChangePassword")
            .WithTags("Authentication")
            .WithSummary("Cambiar contraseña del usuario autenticado")
            .Produces<ChangePasswordResponse>(StatusCodes.Status200OK)
            .Produces<ChangePasswordResponse>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);
        }
    }
}