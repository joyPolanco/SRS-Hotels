using Microsoft.EntityFrameworkCore;
using SRS_Hotels.Data.src.BuildingBlocks.Abstractions;

namespace SRS_Hotels.Data.src.Modules.Accomodation.Features.RoomCategory.GetRoomCategoryById
{
    public record GetRoomCategoryByIdQuery(Guid Id)
       : IQuery<GetRoomCategoryByIdResult>;

    public record RoomCategoryDto(
        Guid Id,
        string Name,
        decimal BasePrice,
        string? Description
    );

    public record GetRoomCategoryByIdResult(
        RoomCategoryDto? Item,
        bool Found
    );
    public class GetRoomCategoryByIdHandler
       : IQueryHandler<GetRoomCategoryByIdQuery, GetRoomCategoryByIdResult>
    {
        private readonly IRepository<Domain.RoomCategory> _repository;

        public GetRoomCategoryByIdHandler(IRepository<Domain.RoomCategory> repository)
        {
            _repository = repository;
        }

        public async Task<GetRoomCategoryByIdResult> Handle(
            GetRoomCategoryByIdQuery request,
            CancellationToken cancellationToken)
        {
            var category = await _repository
                .GetAllAsQueryable()
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (category is null)
            {
                return new GetRoomCategoryByIdResult(null, false);
            }

            var dto = new RoomCategoryDto(
                category.Id,
                category.Name,
                category.BasePrice,
                category.Description
            );

            return new GetRoomCategoryByIdResult(dto, true);
        }
    }
}
