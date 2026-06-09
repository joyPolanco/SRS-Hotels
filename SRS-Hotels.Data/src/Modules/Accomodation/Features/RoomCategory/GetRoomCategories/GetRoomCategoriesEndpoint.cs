using Carter;
using MediatR;

namespace SRS_Hotels.Data.src.Modules.Accomodation.Features.RoomCategory.GetRoomCategories
{
    public record GetRoomCategoriesRequest();

    public record RoomCategoryResponse(
        Guid Id,
        string Name,
        decimal BasePrice,
        string? Description
    );

    public record GetRoomCategoriesResponse(
        List<RoomCategoryResponse> Items
    );


    public class GetRoomCategoriesEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/room-categories", async (
                ISender sender,
                CancellationToken ct) =>
            {
                // ejecutar query
                var result = await sender.Send(new GetRoomCategoriesQuery(), ct);

                // mapping a response HTTP
                var response = new GetRoomCategoriesResponse(
                    result.Items.Select(x => new RoomCategoryResponse(
                        x.Id,
                        x.Name,
                        x.BasePrice,
                        x.Description
                    )).ToList()
                );

                return Results.Ok(response);
            })
            .WithName("GetRoomCategories")
            .WithTags("RoomCategories")
            .Produces<GetRoomCategoriesResponse>(StatusCodes.Status200OK)
            .WithSummary("Obtener categorías de habitaciones")
            .WithDescription("Devuelve todas las categorías disponibles con su información básica.");
        }
    }
}
