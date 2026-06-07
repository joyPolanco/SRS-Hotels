using Carter;
using MediatR;

namespace SRS_Hotels.Data.src.Modules.Authentication.Features.SignIn
{

    public record SignInRequest(string Email, string Password, string FullName);
    public record SignInResponse(bool Successful, string Message);
    public class SignInEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
           app.MapPost("/api/auth/signin", async (SignInRequest request, ISender sender) =>
            {
                var command = new SignInCommand(request.Email, request.Password, request.FullName);
                var result = await sender.Send(command);
                if (result.succesful)
                {
                    return Results.Ok(new SignInResponse(true, "Sign-in successful."));
                }
                else
                {
                    return Results.BadRequest(new SignInResponse(false, result.Message));
                }
            })
            .WithName("SignIn")
            .WithTags("Authentication");
        }
    }
}
