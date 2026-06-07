using SRS_Hotels.Data.src.BuildingBlocks.Abstractions;
using SRS_Hotels.Data.src.BuildingBlocks.Abstractions.SRS_Hotels.Data.src.BuildingBlocks.Abstractions;

namespace SRS_Hotels.Data.src.Modules.Authentication.Features.SignIn
{

    public record SignInCommand(string Email, string Password, string FullName): ICommand<SignInResult>;
    public record SignInResult(bool succesful, string Message);

    public class SignInHandler (IIdentityAccountService accountService): ICommandHandler<SignInCommand, SignInResult>
    {
        public async Task<SignInResult> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var result = await accountService.RegisterUserAsync(request.Email, request.Password, request.FullName);
            {
                return new SignInResult(true, "User registered successfully.");





            }
        }
    }
}
