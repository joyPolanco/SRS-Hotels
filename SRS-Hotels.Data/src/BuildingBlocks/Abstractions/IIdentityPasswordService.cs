namespace SRS_Hotels.Data.src.BuildingBlocks.Abstractions
{
    public interface IIdentityPasswordService
    {
        Task<string> GeneratePasswordResetTokenAsync(Guid userId);
        Task ResetPasswordAsync(Guid userId, string token, string newPassword);
    }
}
