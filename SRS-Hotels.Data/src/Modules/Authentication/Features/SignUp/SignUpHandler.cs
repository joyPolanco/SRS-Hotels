using FluentValidation;
using SRS_Hotels.Data.src.BuildingBlocks.Abstractions;
using SRS_Hotels.Data.src.Modules.Customers.Domain;



public record SignUpCommand(
    string Email,
    string Password,
    string FullName,
    string phoneNumber

) : ICommand<SignUpResult>;

public record SignUpResult(bool Successful, string Message);
public class SignUpHandler : ICommandHandler<SignUpCommand, SignUpResult>
{
    private readonly IIdentityAccountService _accountService;
    private readonly IRepository<Customer> _customerRepository;

    public SignUpHandler(
        IIdentityAccountService accountService,
        IRepository<Customer> customerRepository)
    {
        _accountService = accountService;
        _customerRepository = customerRepository;
    }

    public async Task<SignUpResult> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {

        //  Crear usuario en el sistema de autenticación
         Guid userId = await _accountService.RegisterClientAsync(
            request.Email,
            request.Password,
            request.FullName,
            request.phoneNumber
           
        );
        // Crear Customer
        var customer = new Customer
        {
            Id = userId,
            FullName = request.FullName,
            Email = request.Email,
            Phone = request.phoneNumber,

            Address = null!,
            EmergencyContact = null!,
            LoyaltyAccount = new LoyaltyAccount()
            {
                    Points = 0,
                    Level = LoyaltyLevel.Bronze,
                    CustomerId= userId
                     
            },

            CreatedAt = DateTime.UtcNow
        };

        // Guardar en base de datos
        await _customerRepository.AddAsync(customer);

        return new SignUpResult(true, "User registered successfully.");
    }


    public class SignUpValidation : AbstractValidator<SignUpCommand>
    {
        public SignUpValidation()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El correo es obligatorio.")
                .EmailAddress().WithMessage("Formato de correo inválido.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es obligatoria.")
                .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres.")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).+$")
                .WithMessage("La contraseña debe contener mayúscula, minúscula, número y carácter especial.");

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("El nombre completo es obligatorio.");

            RuleFor(x => x.phoneNumber)
                .NotEmpty().WithMessage("El número de teléfono es obligatorio.")
                .Matches(@"^\+?[1-9]\d{1,14}$")
                .WithMessage("Formato de teléfono inválido.");
        }
    }
}