using MediatR;
using SRS_Hotels.Data.src.BuildingBlocks.Abstractions;
using SRS_Hotels.Data.src.Modules.Accomodation.Domain.Events;

namespace SRS_Hotels.Data.src.Modules.Accomodation.Features.RoomCategory.DeleteRoomCategory
{
    public record DeleteRoomCategoryCommand(Guid Id)
    : ICommand<DeleteRoomCategoryResult>;

    public record DeleteRoomCategoryResult(
        bool Successful,
        string Message
    );
    public class DeleteRoomCategoryHandler
    : ICommandHandler<DeleteRoomCategoryCommand, DeleteRoomCategoryResult>
    {
        private readonly IRepository<Domain.RoomCategory> _repository;
        private readonly IMediator _mediator;

        public DeleteRoomCategoryHandler(
            IRepository<Domain.RoomCategory> repository,
            IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<DeleteRoomCategoryResult> Handle(
            DeleteRoomCategoryCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);

            if (entity is null)
            {
                return new DeleteRoomCategoryResult(false, "Categoría no encontrada");
            }

            await _repository.DeleteAsync(entity);

            await _mediator.Publish(new RoomCategoryDeleted(entity.Id), cancellationToken);

            return new DeleteRoomCategoryResult(true, "Categoría eliminada correctamente");
        }
    }
}
