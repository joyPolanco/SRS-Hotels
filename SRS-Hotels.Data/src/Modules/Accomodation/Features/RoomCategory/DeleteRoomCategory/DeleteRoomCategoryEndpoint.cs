using Carter;
using MediatR;

namespace SRS_Hotels.Data.src.Modules.Accomodation.Features.RoomCategory.DeleteRoomCategory
{
    public record DeleteRoomCategoryRequest(Guid Id);

    public record DeleteRoomCategoryResponse(
        bool Successful,
        string Message
    );
    public class DeleteRoomCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/api/room-categories/{id:guid}", async (
                Guid id,
                ISender sender,
                CancellationToken ct) =>
            {
                var command = new DeleteRoomCategoryCommand(id);

                var result = await sender.Send(command, ct);

                var response = new DeleteRoomCategoryResponse(
                    result.Successful,
                    result.Message
                );

                return result.Successful
                    ? Results.Ok(response)
                    : Results.BadRequest(response);
            })
            .RequireAuthorization(policy => policy.RequireRole("Admin"))
            .WithName("DeleteRoomCategory")
            .WithTags("RoomCategories")
            .Produces<DeleteRoomCategoryResponse>(200)
            .Produces<DeleteRoomCategoryResponse>(400)
            .WithSummary("Eliminar categoría")
            .WithDescription("Elimina una categoría de habitación (solo Admin).");
        }
    }
}
