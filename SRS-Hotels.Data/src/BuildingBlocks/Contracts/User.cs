namespace SRS_Hotels.Data.src.BuildingBlocks.Contracts
{
    public class User
    {
        public Guid Id { get; set; }
        public required string FullName { get; set; }

        public List<string> Roles {  get; set; } = new List<string>();

        public required string Email {  get; set; }
        public bool IsBlocked { get; set; }

        public DateTimeOffset? BlockEnd { get; set; }
    }
}
