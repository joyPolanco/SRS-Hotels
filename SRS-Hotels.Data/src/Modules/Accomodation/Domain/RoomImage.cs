namespace SRS_Hotels.Data.src.Modules.Accomodation.Domain
{
    public class RoomImage
    {
        public Guid Id { get; set; }

        public Guid RoomId { get; set; }

        public string Url { get; set; } = null!;
        public string? Description { get; set; }

        public bool IsMain { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
