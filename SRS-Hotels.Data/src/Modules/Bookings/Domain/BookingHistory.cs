namespace SRS_Hotels.Data.src.Modules.Bookings.Domain
{
    public class BookingHistory
    {
        public Guid Id { get; set; }

        public Guid BookingId { get; set; }

        public string Action { get; set; } = null!;
        public string? Details { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
