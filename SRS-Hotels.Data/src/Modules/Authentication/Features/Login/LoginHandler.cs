using FluentValidation;
using SRS_Hotels.Data.src.BuildingBlocks.Abstractions;
using SRS_Hotels.Data.src.BuildingBlocks.Contracts.SRS_Hotels.Data.src.BuildingBlocks.Contracts;
using SRS_Hotels.Data.src.Modules.Authentication.Domain;

namespace SRS_Hotels.Data.src.Modules.Authentication.Features.Login
{
    public enum AuthStatus
    {
        Success,
        InvalidCredentials,
        LockedOut
    }
    public record LoginCommand(string Email, string Password): ICommand<LoginResult>;

    public record LoginResult(string? Token,AuthStatus AuthStatus,bool Successful, string Message, string? RefreshToken);
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
    public class LoginHandler(
       IJwtTokenService jwtTokenService,
       IIdentityAccountService accountService,
       IRepository<RefreshToken> repository)
       : ICommandHandler<LoginCommand, LoginResult>
    {
        public async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await accountService.CheckPasswordAsync(request.Email, request.Password);

            if (user == null)
            {
               
                return new LoginResult(Token:null, AuthStatus.InvalidCredentials,false, Message: "Credenciales inválidas", null);
            }

            if (user.IsBlocked)
            {
               

                return new LoginResult(Token: null, AuthStatus.LockedOut, false,Message: $"Cuenta bloqueada hasta {user.BlockEnd}", null);
            }

            var jwt = jwtTokenService.GenerateJwtToken(new GenerateJwtRequest
            {
                FullName = user.FullName,
                Roles = user.Roles,
                Email = user.Email,
                UserId = user.Id
            });

            //generar refresh token
            var refreshToken = new RefreshToken
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Token = Guid.NewGuid().ToString("N"),
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                IsUsed = false
            };

            await repository.AddAsync(refreshToken);

         
            return new LoginResult(jwt, AuthStatus.Success,true, "OK", refreshToken.Token);
        }
    }
}

