namespace SRS_Hotels.Data.src.Modules.Accomodation.Domain
{
    public class RoomCategory
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        public decimal BasePrice { get; set; }
    }
}
