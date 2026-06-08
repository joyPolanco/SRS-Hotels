namespace SRS_Hotels.Data.src.BuildingBlocks.Abstractions
{
    public interface IIdentityPasswordService
    {
        Task<string> GeneratePasswordResetTokenAsync(Guid userId);
        Task ResetPasswordByTokenAsync(Guid userId, string token, string newPassword);
        Task ResetPasswordAsync(Guid userId, string currentPassword, string newPassword);

    }
}
