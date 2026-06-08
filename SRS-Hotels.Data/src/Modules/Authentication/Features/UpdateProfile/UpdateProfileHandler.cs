
using FluentValidation;
using SRS_Hotels.Data.src.BuildingBlocks.Abstractions;
using SRS_Hotels.Data.src.Modules.Customers.Domain;

namespace SRS_Hotels.Data.src.Modules.Authentication.Features.UpdateProfile
{

    public class AddressDto
    {
        public string Country { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Street { get; set; } = default!;
    }
    public record UpdateProfileResult(
    UpdateProfileStatus Status,
    string Message,
    Guid? UserId = null,
    string? FullName = null
);
    public class EmergencyContactDto
    {
        public string Name { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string Relationship { get; set; } = default!;
    }


    public record UpdateProfileCommand(
        Guid UserId,
        string FullName,
        AddressDto? Address,
        EmergencyContactDto? EmergencyContact) : ICommand<UpdateProfileResult>;

    public enum UpdateProfileStatus
    {
        Success,
        Blocked,
        Failed,
        NotFound
    };
    public class UpdateProfileHandler : ICommandHandler<UpdateProfileCommand, UpdateProfileResult>
    {
        private readonly IRepository<Customer> _repository;
        private readonly IIdentityAccountService _identity;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRetryPolicy _retryPolicy;

        public UpdateProfileHandler(
            IRepository<Customer> repository,
            IIdentityAccountService identity,
            IUnitOfWork unitOfWork
            )

        {
            _repository = repository;
            _identity = identity;
            _unitOfWork = unitOfWork;
        }
        public async Task<UpdateProfileResult> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByIdAsync(request.UserId);

            if (user is null)
                return new UpdateProfileResult(
                    UpdateProfileStatus.NotFound,
                    "User not found"
                );

            user.FullName = request.FullName;

            if (request.Address != null)
            {
                user.Address = new Address
                {
                    Country = request.Address.Country,
                    City = request.Address.City,
                    Street = request.Address.Street
                };
            }

            if (request.EmergencyContact != null)
            {
                user.EmergencyContact = new EmergencyContact
                {
                    Name = request.EmergencyContact.Name,
                    Phone = request.EmergencyContact.Phone,
                    Relationship = request.EmergencyContact.Relationship
                };
            }

              var identityResult= await _identity.UpdateUserAsync(request.UserId, request.FullName);
           

            if (identityResult.UserBlocked)
            {
                return new UpdateProfileResult(
                    UpdateProfileStatus.Blocked,
                    "User is blocked"
                );
            }

            if (!identityResult.Success)
            {
                return new UpdateProfileResult(
                    UpdateProfileStatus.Failed,
                    "Identity update failed"
                );
            }

        
            await _repository.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new UpdateProfileResult(
                UpdateProfileStatus.Success,
                "Profile updated successfully",
                user.Id,
                user.FullName
            );
        }



      
    }

    public class UpdateProfileValidator : AbstractValidator<UpdateProfileCommand>
    {
        public UpdateProfileValidator()
        {
            RuleFor(x => x.UserId)
                .NotEqual(Guid.Empty)
                .WithMessage("UserId must be provided.");

            RuleFor(x => x.FullName)
                .NotEmpty()
                .WithMessage("FullName must not be empty.")
                .MaximumLength(200)
                .WithMessage("FullName must not exceed 200 characters.");

            When(x => x.Address != null, () =>
            {
                RuleFor(x => x.Address!).SetValidator(new AddressDtoValidator());
            });

            When(x => x.EmergencyContact != null, () =>
            {
                RuleFor(x => x.EmergencyContact!).SetValidator(new EmergencyContactDtoValidator());
            });
        }
    }

    internal class AddressDtoValidator : AbstractValidator<AddressDto>
    {
        public AddressDtoValidator()
        {
            RuleFor(a => a.Country)
                .NotEmpty()
                .WithMessage("Country must not be empty.")
                .MaximumLength(100);

            RuleFor(a => a.City)
                .NotEmpty()
                .WithMessage("City must not be empty.")
                .MaximumLength(100);

            RuleFor(a => a.Street)
                .NotEmpty()
                .WithMessage("Street must not be empty.")
                .MaximumLength(200);
        }
    }

    internal class EmergencyContactDtoValidator : AbstractValidator<EmergencyContactDto>
    {
        public EmergencyContactDtoValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty()
                .WithMessage("Emergency contact name must not be empty.")
                .MaximumLength(150);

            RuleFor(e => e.Phone)
                .NotEmpty()
                .WithMessage("Emergency contact phone must not be empty.")
                .Matches(@"^\+?[0-9\s\-]{7,20}$")
                .WithMessage("Phone must contain only digits, spaces, dashes and an optional leading + (7-20 chars).");

            RuleFor(e => e.Relationship)
                .NotEmpty()
                .WithMessage("Emergency contact relationship must not be empty.")
                .MaximumLength(100);
        }
    }
}
