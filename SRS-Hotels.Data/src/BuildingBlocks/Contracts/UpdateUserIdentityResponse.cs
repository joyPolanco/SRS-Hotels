namespace SRS_Hotels.Data.src.BuildingBlocks.Contracts
{
    public class UpdateUserIdentityResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;

        public bool UserBlocked {  get; set; }
    }
}
