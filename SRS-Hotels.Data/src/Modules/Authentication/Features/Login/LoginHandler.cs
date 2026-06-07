using FluentValidation;
using SRS_Hotels.Data.src.BuildingBlocks.Abstractions;

namespace SRS_Hotels.Data.src.Modules.Authentication.Features.Login
{

    public record LoginCommand(string Email, string Password): ICommand<LoginResult>;

    public record LoginResult(string Token, DateTime Expiration);
    public class LoginValidator : AbstractValidator<LoginCommand>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
        }
    }
    public class LoginHandler : ICommandHandler<LoginCommand, LoginResult>
    {
        public Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
