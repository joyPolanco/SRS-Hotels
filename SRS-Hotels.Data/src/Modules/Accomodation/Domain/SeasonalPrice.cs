namespace SRS_Hotels.Data.src.Modules.Accomodation.Domain
{
    public class SeasonalPrice
    {
        public Guid Id { get; set; }

        public Guid RoomId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public decimal Price { get; set; }
    }
}
