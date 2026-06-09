using Carter;
using Mapster;
using MediatR;

namespace SRS_Hotels.Data.src.Modules.Accomodation.Features.RoomCategory.CreateRoomCategory
{

    public record CreateRoomCategoryRequest(
        string Name,
        decimal BasePrice,
        string? Description
    );

    public record CreateRoomCategoryResponse(
        bool Successful,
        string Message
    );

    public class CreateRoomCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/api/room-categories", async (
                CreateRoomCategoryRequest request,
                ISender sender,
                CancellationToken ct) =>
            {
                // mapping request → command
                var command = request.Adapt<CreateCategoryCommand>();

                // ejecutar caso de uso
                var result = await sender.Send(command, ct);

                // mapping response
                var response = new CreateRoomCategoryResponse(
                    result.successful,
                    result.successful
                        ? "Categoría creada correctamente"
                        : "No se pudo crear la categoría"
                );

                return Results.Ok(response);
            })
             .RequireAuthorization(policy => policy.RequireRole("Administrator"))

            .WithName("CreateRoomCategory")
            .WithTags("RoomCategories")
            .Produces<CreateRoomCategoryResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithSummary("Crear categoría de habitación")
            .WithDescription("Crea una nueva categoría con precio base y descripción opcional.");
        }
    }
}
