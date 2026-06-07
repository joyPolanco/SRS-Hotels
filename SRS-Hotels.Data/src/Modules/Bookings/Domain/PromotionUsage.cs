namespace SRS_Hotels.Data.src.Modules.Bookings.Domain
{
    public class PromotionUsage
    {
        public Guid Id { get; set; }

        public Guid PromotionId { get; set; }
        public Guid BookingId { get; set; }
        public Guid CustomerId { get; set; }

        public DateTime UsedAt { get; set; }
    }
}
