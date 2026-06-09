using Carter;
using MediatR;

namespace SRS_Hotels.Data.src.Modules.Accomodation.Features.RoomCategory.GetRoomCategoryById
{
    public record GetRoomCategoryByIdRequest(Guid Id);

    public record GetRoomCategoryByIdResponse(
        bool Found,
        RoomCategoryDto? Item
    );
    public class GetRoomCategoryByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/room-categories/{id:guid}", async (
                Guid id,
                ISender sender,
                CancellationToken ct) =>
            {
                var query = new GetRoomCategoryByIdQuery(id);

                var result = await sender.Send(query, ct);

                var response = new GetRoomCategoryByIdResponse(
                    result.Found,
                    result.Item
                );

                return result.Found
                    ? Results.Ok(response)
                    : Results.NotFound(response);
            })
            .WithName("GetRoomCategoryById")
            .WithTags("RoomCategories")
            .Produces<GetRoomCategoryByIdResponse>(200)
            .Produces<GetRoomCategoryByIdResponse>(404)
            .WithSummary("Obtener categoría por ID")
            .WithDescription("Devuelve una categoría específica de habitación.");
        }
    }
}
