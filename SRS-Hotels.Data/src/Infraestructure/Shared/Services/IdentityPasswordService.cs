namespace SRS_Hotels.Data.src.Infraestructure.Shared.Services
{
    using global::SRS_Hotels.Data.src.BuildingBlocks.Abstractions;
    using global::SRS_Hotels.Data.src.Infraestructure.Identity.Entities;
    using Microsoft.AspNetCore.Identity;

    namespace SRS_Hotels.Infrastructure.Authentication
    {
        public class IdentityPasswordService : IIdentityPasswordService
        {
            private readonly UserManager<ApplicationUser> _userManager;

            public IdentityPasswordService(UserManager<ApplicationUser> userManager)
            {
                _userManager = userManager;
            }

            public async Task<string> GeneratePasswordResetTokenAsync(Guid userId)
            {
                var user = await _userManager.FindByIdAsync(userId.ToString());

                if (user == null)
                    throw new Exception("User not found");

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                return token;
            }

            public async Task ResetPasswordAsync(Guid userId, string token, string newPassword)
            {
                var user = await _userManager.FindByIdAsync(userId.ToString());

                if (user == null)
                    throw new Exception("User not found");

                var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    throw new Exception($"Password reset failed: {errors}");
                }
            }
        }
    }
}
