using MediatR;
using Microsoft.EntityFrameworkCore;
using SRS_Hotels.Data.src.BuildingBlocks.Abstractions;
using SRS_Hotels.Data.src.Modules.Shared.Projections.SRS_Hotels.Data.src.Projections;

namespace SRS_Hotels.Data.src.Modules.Accomodation.Domain.Events
{
    public record RoomCategoryDeleted(Guid CategoryId) : INotification;

    public class RoomCategoryDeletedHandler
        : INotificationHandler<RoomCategoryDeleted>
    {
        private readonly IRepository<CategoryBookingIndex> _indexRepository;

        public RoomCategoryDeletedHandler(
            IRepository<CategoryBookingIndex> indexRepository)
        {
            _indexRepository = indexRepository;
        }

        public async Task Handle(
            RoomCategoryDeleted notification,
            CancellationToken cancellationToken)
        {
            var index = await _indexRepository
                .GetAllAsQueryable()
                .FirstOrDefaultAsync(x => x.CategoryId == notification.CategoryId, cancellationToken);

            if (index is null)
                return;

            await _indexRepository.DeleteAsync(index);
        }
    }
}
