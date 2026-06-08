using Microsoft.EntityFrameworkCore;
using SRS_Hotels.Data.src.BuildingBlocks.Abstractions;
using SRS_Hotels.Data.src.BuildingBlocks.Contracts.SRS_Hotels.Data.src.BuildingBlocks.Contracts;

namespace SRS_Hotels.Data.src.Modules.Authentication.Features.RefreshToken
{
    public record RefreshTokenCommand(string RefreshToken)
     : ICommand<RefreshTokenResult>;

    public record RefreshTokenResult(
        string AccessToken,
        string RefreshToken,
        bool Successful,
        string Message
    );

    public class RefreshTokenHandler(
       IRepository<Domain.RefreshToken> refreshTokenRepository,
       IIdentityAccountService identityService,
       IJwtTokenService jwtService)
       : ICommandHandler<RefreshTokenCommand, RefreshTokenResult>
    {
        public async Task<RefreshTokenResult> Handle(
            RefreshTokenCommand request,
            CancellationToken cancellationToken)
        {
            //  buscar token
            var storedToken = await refreshTokenRepository
                .GetAllAsQueryable()
                .FirstOrDefaultAsync(
                    x => x.Token == request.RefreshToken,
                    cancellationToken);

            //  token no existe
            if (storedToken is null)
            {
                return new RefreshTokenResult(
                    "",
                    "",
                    false,
                    "Refresh token inválido"
                );
            }

            //  REUSE ATTACK DETECTED
            if (!storedToken.IsActive)
            {
                //  revocar todos los tokens del usuario
                var allUserTokens = refreshTokenRepository
                    .GetAllAsQueryable()
                    .Where(x => x.UserId == storedToken.UserId && x.RevokedAt == null);

                foreach (var token in allUserTokens)
                {
                    token.IsUsed = true;
                    token.RevokedAt = DateTime.UtcNow;
                }

                return new RefreshTokenResult(
                    "",
                    "",
                    false,
                    "Se detectó reutilización de refresh token. Sesión comprometida."
                );
            }

            //  obtener usuario desde Identity
            var user = await identityService.GetUserByIdAsync(storedToken.UserId);

            if (user is null)
            {
                return new RefreshTokenResult(
                    "",
                    "",
                    false,
                    "Usuario no encontrado"
                );
            }

            // generar nuevo JWT
            var newAccessToken = jwtService.GenerateJwtToken(new GenerateJwtRequest
            {
                UserId = user.Id,
                Email = user.Email!,
                FullName = user.FullName
            });

            // rotar token actual
            storedToken.IsUsed = true;
            storedToken.RevokedAt = DateTime.UtcNow;

            var newRefreshToken = new Domain.RefreshToken
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Token = Guid.NewGuid().ToString("N"),
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                IsUsed = false
            };

            await refreshTokenRepository.AddAsync(newRefreshToken);

            storedToken.ReplacedByTokenId = newRefreshToken.Id;

            return new RefreshTokenResult(
                newAccessToken,
                newRefreshToken.Token,
                true,
                "Token renovado correctamente"
            );
        }
    }
}
