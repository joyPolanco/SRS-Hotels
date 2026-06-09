using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SRS_Hotels.Data.src.BuildingBlocks.Abstractions;
using SRS_Hotels.Data.src.Modules.Shared.Projections.SRS_Hotels.Data.src.Projections;

namespace SRS_Hotels.Data.src.Modules.Accomodation.Features.RoomCategory.UpdateRoomCategory
{
    public record UpdateRoomCategoryCommand(
    Guid Id,
    string Name,
    decimal BasePrice,
    string? Description
) : ICommand<UpdateRoomCategoryResult>;

    public record UpdateRoomCategoryResult(
        bool Successful,
        string Message
    );
    public class UpdateRoomCategoryHandler
        : ICommandHandler<UpdateRoomCategoryCommand, UpdateRoomCategoryResult>
    {
        private readonly IRepository<Domain.RoomCategory> _repository;
        private readonly IRepository<CategoryBookingIndex> _indexRepository;

        public UpdateRoomCategoryHandler(
            IRepository<Domain.RoomCategory> repository,
            IRepository<CategoryBookingIndex> indexRepository)
        {
            _repository = repository;
            _indexRepository = indexRepository;
        }

        public async Task<UpdateRoomCategoryResult> Handle(
            UpdateRoomCategoryCommand request,
            CancellationToken cancellationToken)
        {
            var category = await _repository.GetByIdAsync(request.Id);

            if (category is null)
                return new UpdateRoomCategoryResult(false, "Categoría no encontrada");

            // índice de reservas
            var index = await _indexRepository
                .GetAllAsQueryable()
                .FirstOrDefaultAsync(x => x.CategoryId == request.Id, cancellationToken);

            if (index?.HasBookings == true && category.BasePrice != request.BasePrice)
            {
                return new UpdateRoomCategoryResult(
                    false,
                    "No se puede modificar el precio porque la categoría ya tiene reservas"
                );
            }

            //ACTUALIZACIÓN
            category.Name = request.Name;

            if (index?.HasBookings != true)
            {
                category.BasePrice = request.BasePrice;
            }

            category.Description = request.Description;

            await _repository.UpdateAsync(category);

            return new UpdateRoomCategoryResult(true, "Categoría actualizada correctamente");
        }
    }

    public class UpdateRoomCategoryValidator
      : AbstractValidator<UpdateRoomCategoryCommand>
    {
        public UpdateRoomCategoryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("El ID es obligatorio.");

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(150)
                .WithMessage("El nombre es obligatorio.");

            RuleFor(x => x.BasePrice)
                .GreaterThan(0)
                .WithMessage("El precio debe ser mayor que 0.");

            When(x => x.Description != null, () =>
            {
                RuleFor(x => x.Description!)
                    .MaximumLength(500);
            });
        }
    }
}
