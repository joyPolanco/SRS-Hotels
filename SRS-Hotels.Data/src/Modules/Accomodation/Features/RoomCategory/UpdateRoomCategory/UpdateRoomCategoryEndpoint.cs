using Carter;
using Mapster;
using MediatR;

namespace SRS_Hotels.Data.src.Modules.Accomodation.Features.RoomCategory.UpdateRoomCategory
{
    public record UpdateRoomCategoryRequest(
    Guid Id,
    string Name,
    decimal BasePrice,
    string? Description
);

    public record UpdateRoomCategoryResponse(
        bool Successful,
        string Message
    );
    public class UpdateRoomCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/api/room-categories", async (
                UpdateRoomCategoryRequest request,
                ISender sender,
                CancellationToken ct) =>
            {
                var command = request.Adapt<UpdateRoomCategoryCommand>();

                var result = await sender.Send(command, ct);

                var response = new UpdateRoomCategoryResponse(
                    result.Successful,
                    result.Message
                );

                return result.Successful
                    ? Results.Ok(response)
                    : Results.BadRequest(response);
            })
             .RequireAuthorization(policy => policy.RequireClaim("Administrator"))
            .WithName("UpdateRoomCategory")
            .WithTags("RoomCategories")
            .Produces<UpdateRoomCategoryResponse>(200)
            .Produces<UpdateRoomCategoryResponse>(400)
            .WithSummary("Actualizar categoría")
            .WithDescription("Actualiza una categoría de habitación. No permite cambiar precio si ya tiene reservas.");
        }
    }
}
