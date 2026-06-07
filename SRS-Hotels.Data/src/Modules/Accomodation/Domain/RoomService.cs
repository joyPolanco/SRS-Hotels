namespace SRS_Hotels.Data.src.Modules.Accomodation.Domain
{
    public class RoomService
    {
        public Guid Id { get; set; }

        public Guid RoomId { get; set; }

        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        public decimal Price { get; set; }

        public bool IsAvailable { get; set; }
    }
}
