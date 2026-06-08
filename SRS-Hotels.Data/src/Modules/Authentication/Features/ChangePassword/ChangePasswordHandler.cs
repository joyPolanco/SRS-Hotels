using FluentValidation;
using SRS_Hotels.Data.src.BuildingBlocks.Abstractions;

namespace SRS_Hotels.Data.src.Modules.Authentication.Features.ChangePassword
{
 
    public record ChangePasswordResult(bool Successful, string Message);

    public record ChangePasswordCommand(
        Guid UserId,
        string CurrentPassword,
        string NewPassword
    ) : ICommand<ChangePasswordResult>;

    public class ChangePasswordHandler(IIdentityAccountService accountService)
        : ICommandHandler<ChangePasswordCommand, ChangePasswordResult>
    {
        public async Task<ChangePasswordResult> Handle(
            ChangePasswordCommand request,
            CancellationToken cancellationToken)
        {
            var result = await accountService.ChangePasswordAsync(
                request.UserId,
                request.CurrentPassword,
                request.NewPassword
            );

            return new ChangePasswordResult(
                result.Successful,
                result.Message
            );
        }
    }


    public class ChangePasswordValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.UserId)
                .NotEqual(Guid.Empty)
                .WithMessage("El usuario es obligatorio.");

            RuleFor(x => x.CurrentPassword)
                .NotEmpty()
                .WithMessage("La contraseña actual es obligatoria.")
                .MinimumLength(6)
                .WithMessage("La contraseña actual no es válida.");

            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .WithMessage("La nueva contraseña es obligatoria.")
                .MinimumLength(6)
                .WithMessage("La nueva contraseña debe tener al menos 6 caracteres.")
                .NotEqual(x => x.CurrentPassword)
                .WithMessage("La nueva contraseña no puede ser igual a la anterior.");
        }
    }
}