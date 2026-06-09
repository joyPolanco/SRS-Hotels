using MediatR;
using SRS_Hotels.Data.src.BuildingBlocks.Abstractions;
using SRS_Hotels.Data.src.Modules.Shared.Projections.SRS_Hotels.Data.src.Projections;

namespace SRS_Hotels.Data.src.Modules.Accomodation.Domain.Events
{
    public record CategoryCreated(Guid CategoryId) : INotification;
    public class CategoryCreatedHandler
    : INotificationHandler<CategoryCreated>
    {
        private readonly IRepository<CategoryBookingIndex> _indexRepository;

        public CategoryCreatedHandler(IRepository<CategoryBookingIndex> indexRepository)
        {
            _indexRepository = indexRepository;
        }

        public async Task Handle(CategoryCreated notification, CancellationToken cancellationToken)
        {
            var index = new CategoryBookingIndex
            {
                Id = Guid.NewGuid(),
                CategoryId = notification.CategoryId,
                HasBookings = false,
                TotalBookings = 0,
                UpdatedAt = DateTime.UtcNow
            };

            await _indexRepository.AddAsync(index);
        }
    }
}
