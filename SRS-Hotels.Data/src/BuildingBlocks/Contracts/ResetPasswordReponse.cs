namespace SRS_Hotels.Data.src.BuildingBlocks.Contracts
{
    public class ResetPasswordResponse
    {
        public bool UserIsBlocked { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool Successful { get; set; }
    }
}
