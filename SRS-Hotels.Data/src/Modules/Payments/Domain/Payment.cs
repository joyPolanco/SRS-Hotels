using SRS_Hotels.Data.src.Modules.Bookings.Domain;
using SRS_Hotels.Data.src.Modules.Payments.Domain.SRS_Hotels.Modules.Payments.Domain;

namespace SRS_Hotels.Modules.Payments.Domain
{
    public class Payment
    {
        public Guid Id { get; set; }

        public Guid BookingId { get; set; }

        public Guid PaymentMethodId { get; set; }

        public decimal Amount { get; set; }

        public PaymentStatus Status { get; set; }

        public string? TransactionReference { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? PaidAt { get; set; }

        public Booking Booking { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}