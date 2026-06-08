using Carter;
using MediatR;
using Mapster;

namespace SRS_Hotels.Data.src.Modules.Authentication.Features.RefreshToken
{
 
    public record RefreshTokenRequest(string RefreshToken);


    public record RefreshTokenResponse(
        string AccessToken,
        string RefreshToken,
        bool Successful,
        string Message
    );

 
    public class RefreshTokenEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/api/auth/refresh-token", async (
                RefreshTokenRequest request,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var command = request.Adapt<RefreshTokenCommand>();

                var result = await sender.Send(command, cancellationToken);
                var response = request.Adapt<RefreshTokenResponse>();

                return result.Successful
                    ? Results.Ok(response)
                    : Results.BadRequest(response);

            })
            .WithName("RefreshToken")
            .WithTags("Autenticación")
            .WithSummary("Renovar token de acceso")
            .WithDescription("Recibe un refresh token válido y devuelve un nuevo access token y refresh token.")
            .Produces<RefreshTokenResponse>(StatusCodes.Status200OK)
            .Produces<RefreshTokenResponse>(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError);
        }
    }
}