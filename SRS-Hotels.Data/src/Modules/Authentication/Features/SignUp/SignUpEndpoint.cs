using Carter;
using Mapster;
using MediatR;

namespace SRS_Hotels.Data.src.Modules.Authentication.Features.SignUp
{

    public record SignUpRequest(string Email, string Password, string FullName, string PhoneNumber);
    public record SignUpResponse(bool Successful, string Message);
    public class SignUpEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/api/auth/signup", async (
                  SignUpRequest request,
                  ISender sender,
                  HttpContext httpContext) =>
                        {
                            // si ya está autenticado, no permitir signup
                            if (httpContext.User.Identity?.IsAuthenticated == true)
                                return Results.BadRequest("User already authenticated.");

                            var command = request.Adapt<SignUpCommand>();

                            var result = await sender.Send(command);

                            var response = result.Adapt<SignUpResponse>();

                            return Results.Ok(response);
                        })
                        .WithName("SignUp")
                        .WithTags("Authentication")

                        .Produces<SignUpResponse>(StatusCodes.Status200OK)
                        .Produces(StatusCodes.Status400BadRequest)
                        .WithSummary("Registers a new user account.")
                        .WithDescription("Creates a new client account with the provided email, password, full name, and phone number. Returns a success status and message.");


                /*
                   .WithOpenApi(operation =>
        {
                    operation.Summary = "User registration endpoint";
                    operation.Description = "Allows anonymous users to create a new account.";

                    operation.RequestBody = new()
                    {
                        Description = "User registration data",
                        Required = true
                    };

                    return operation;
                    });
                     */


        }
    }
}
