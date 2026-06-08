using Carter;
using MediatR;
using System.Security.Claims;

namespace SRS_Hotels.Data.src.Modules.Authentication.Features.UpdateProfile
{

    public record UpdateProfileRequest(string FullName,
        AddressDto? Address,
        EmergencyContactDto? EmergencyContact);

    public record UpdateProfileResponse(bool SuccessFul, string Message);

    public class UpdateProfileEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/api/auth/profile", async (
                UpdateProfileRequest request,
                HttpContext httpContext,
                ISender sender,
                CancellationToken cancellationToken) =>
            {

                if (httpContext.User.Identity?.IsAuthenticated != true)
                    return Results.Unauthorized();


                var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


                var userId = Guid.Parse(userIdClaim!);

                var command = new UpdateProfileCommand(
                    userId,
                    Address: request.Address,
                    EmergencyContact: request.EmergencyContact,

                    FullName:request.FullName
                );

                var result = await sender.Send(command, cancellationToken);

                return result.Status==UpdateProfileStatus.Success
                    ? Results.Ok(new UpdateProfileResponse(true, result.Message))
                    : Results.BadRequest(new UpdateProfileResponse(false, result.Message));

            }).WithName("UpdateProfile")
                .WithTags("Authentication")
                .WithSummary("Update user profile information")
                .WithDescription("Updates the authenticated user's profile including full name, address and emergency contact.")
                .Produces<UpdateProfileResponse>(StatusCodes.Status200OK)
                .Produces<UpdateProfileResponse>(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status401Unauthorized);
        }
    }
}
