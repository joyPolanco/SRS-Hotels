using SRS_Hotels.Data.src.Modules.Bookings.Domain.Common.Enums;

namespace SRS_Hotels.Data.src.Modules.Bookings.Domain
{
    public class Booking
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }
        public Guid RoomId { get; set; }

        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }

        public BookingStatus Status { get; set; }

        public decimal BaseAmount { get; set; }
        public decimal TotalAmount { get; set; }

        public Guid? PromotionId { get; set; }
        public Guid? CancellationPolicyId { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<BookingGuest> Guests { get; set; } = new List<BookingGuest>();
        public ICollection<BookingHistory> History { get; set; } = new List<BookingHistory>();
    }
}
