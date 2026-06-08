using SRS_Hotels.Data.src.BuildingBlocks.Contracts;
using SRS_Hotels.Data.src.Infraestructure.Identity.Entities;

 namespace SRS_Hotels.Data.src.BuildingBlocks.Abstractions
    {
        public interface IIdentityAccountService
        {
            // REGISTRO
            Task<Guid> RegisterClientAsync(string email, string password, string fullName, string phoneNumber);
            Task<Guid> RegisterEmployeeAsync(string email, string password, string fullName, string phoneNumber);

            // LOGIN 
            Task<User?> CheckPasswordAsync(string email, string password);

            // EMAIL CONFIRMATION
            Task<string> GenerateEmailConfirmationTokenAsync(Guid userId);
            Task ConfirmEmailAsync(Guid userId, string token);

            // RESET PASSWORD
            Task<string> GeneratePasswordResetTokenAsync(Guid userId);

            // USER INFO
            Task<Guid?> GetUserIdByEmailAsync(string email);
            Task<ApplicationUser?> GetUserByIdAsync(Guid userId);

            // ROLES
            Task<IList<string>> GetUserRolesAsync(Guid userId);
            Task AddToRoleAsync(Guid userId, string role);

        Task<ResetPasswordResponse> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);

        Task<UpdateUserIdentityResponse> UpdateUserAsync(Guid id, string fullName);
    }
    }

