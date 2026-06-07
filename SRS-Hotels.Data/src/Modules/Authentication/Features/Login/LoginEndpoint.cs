using Carter;

namespace SRS_Hotels.Data.src.Modules.Authentication.Features.Login
{



    public record LoginRequest(string Email, string Password);
    public record LoginResponse(string Token);


    public class LoginEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/api/auth/login", async (LoginRequest request) =>
            {
             
                return Results.Ok(new LoginResponse("fake-jwt-token"));
            }).WithTags("Authentication").WithName("Login");
        }
    }
}
