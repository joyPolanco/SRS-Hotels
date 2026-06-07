namespace SRS_Hotels.Data.src.Modules.Bookings.Domain
{
    public class BookingCancellation
    {
        public Guid Id { get; set; }

        public Guid BookingId { get; set; }

        public Guid CancelledByUserId { get; set; }

        public DateTime CancelledAt { get; set; }

        public string Reason { get; set; } = null!;

        public decimal RefundAmount { get; set; }

        public decimal PenaltyAmount { get; set; }
    }
}
