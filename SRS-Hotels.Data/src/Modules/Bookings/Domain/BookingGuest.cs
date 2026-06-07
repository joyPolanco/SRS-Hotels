namespace SRS_Hotels.Data.src.Modules.Bookings.Domain
{
    public class BookingGuest
    {
        public Guid Id { get; set; }

        public Guid BookingId { get; set; }

        public string FullName { get; set; } = null!;
        public string? DocumentNumber { get; set; }

        public bool IsMainGuest { get; set; }
    }
}
