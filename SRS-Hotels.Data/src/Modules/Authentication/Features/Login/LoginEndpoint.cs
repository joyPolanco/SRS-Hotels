using Carter;
using Mapster;
using MediatR;

namespace SRS_Hotels.Data.src.Modules.Authentication.Features.Login
{



    public record LoginRequest(string Email, string Password);
    public record LoginResponse(string? Token, bool Successful, string Message, string? RefreshToken);


    public class LoginEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/api/auth/login", async (LoginRequest request, ISender sender, HttpContext httpContext) =>
            {
                Console.WriteLine(httpContext.User.ToString());

                // si ya está autenticado, no permitir signup
                if (httpContext.User.Identity?.IsAuthenticated == true)
                {
                    return Results.BadRequest("User already authenticated.");

                }

                var command = request.Adapt<LoginCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<LoginResponse>();

                return result.AuthStatus switch
                {
                    AuthStatus.Success =>
                        Results.Ok(response),

                    AuthStatus.InvalidCredentials =>
                        Results.Json(
                            new { message = "Credenciales inválidas" },
                            statusCode: StatusCodes.Status401Unauthorized),

                    AuthStatus.LockedOut =>
                        Results.Json(response, statusCode: StatusCodes.Status403Forbidden),

                    _ =>
                        Results.Problem("Unexpected error")
                };
            })
            .WithTags("Authentication")
            .WithDescription("")
            .WithName("Login");
        }
    }
}
