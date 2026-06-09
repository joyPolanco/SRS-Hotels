using FluentValidation;
using MediatR;
using SRS_Hotels.Data.src.BuildingBlocks.Abstractions;
using SRS_Hotels.Data.src.Modules.Accomodation.Domain.Events;

namespace SRS_Hotels.Data.src.Modules.Accomodation.Features.RoomCategory.CreateRoomCategory
{
    public record CreateCategoryCommand(string Name, decimal basePrice, string ?Description) : ICommand<CreateCategoryResult>;
    public record CreateCategoryResult(bool successful);

    public class CreateCategoryHandler : ICommandHandler<CreateCategoryCommand, CreateCategoryResult>
    {
        private readonly IRepository<Domain.RoomCategory> _repository;
        private readonly IMediator _mediator;

        public CreateCategoryHandler(
            IRepository<Domain.RoomCategory> repository,
            IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<CreateCategoryResult> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            //crear entidad dominio
            var category = new Domain.RoomCategory
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                BasePrice = request.basePrice,
               
            };
            if (!String.IsNullOrEmpty(request.Description))
                category.Description = request.Description;

            await _repository.AddAsync(category);


            //  publicar evento
            await _mediator.Publish(new CategoryCreated(category.Id), cancellationToken);

            return new CreateCategoryResult(successful:true);
        }
      
    }

    public class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("El nombre de la categoría es obligatorio.")
                .MaximumLength(150)
                .WithMessage("El nombre no puede superar los 150 caracteres.");

            RuleFor(x => x.basePrice)
                .GreaterThanOrEqualTo(0)
                .WithMessage("El precio base debe ser mayor o igual que 0.");

            When(x => x.Description != null, () =>
            {
                RuleFor(x => x.Description)
                    .MaximumLength(500)
                    .WithMessage("La descripción no puede superar los 500 caracteres.");
            });
        }
    }
}
