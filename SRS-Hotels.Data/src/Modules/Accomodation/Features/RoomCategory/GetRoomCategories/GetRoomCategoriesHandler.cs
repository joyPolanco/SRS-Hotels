using Microsoft.EntityFrameworkCore;
using SRS_Hotels.Data.src.BuildingBlocks.Abstractions;

namespace SRS_Hotels.Data.src.Modules.Accomodation.Features.RoomCategory.GetRoomCategories
{

   
    public record GetRoomCategoriesQuery() : IQuery<GetRoomCategoriesResult>;

    public record RoomCategoryDto(
        Guid Id,
        string Name,
        decimal BasePrice,
        string? Description
    );

    public record GetRoomCategoriesResult(
        List<RoomCategoryDto> Items
    );


    public class GetRoomCategoriesHandler
          : IQueryHandler<GetRoomCategoriesQuery, GetRoomCategoriesResult>
    {
        private readonly IRepository<Domain.RoomCategory> _repository;

        public GetRoomCategoriesHandler(IRepository<Domain.RoomCategory> repository)
        {
            _repository = repository;
        }

        public async Task<GetRoomCategoriesResult> Handle(
            GetRoomCategoriesQuery request,
            CancellationToken cancellationToken)
        {
            var categories =  _repository.GetAllAsQueryable();

            var result = await categories
                .Select(x => new RoomCategoryDto(
                    x.Id,
                    x.Name,
                    x.BasePrice,
                    x.Description
                ))
                .ToListAsync();

            return new GetRoomCategoriesResult(result);
        }
    }
}
