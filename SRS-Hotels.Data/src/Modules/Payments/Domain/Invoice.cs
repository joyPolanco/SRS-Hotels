using SRS_Hotels.Data.src.Modules.Bookings.Domain;

namespace SRS_Hotels.Data.src.Modules.Payments.Domain
{
    namespace SRS_Hotels.Modules.Payments.Domain
    {
        public class Invoice
        {
            public Guid Id { get; set; }

            public Guid BookingId { get; set; }

            public string InvoiceNumber { get; set; }

            public decimal SubTotal { get; set; }

            public decimal Taxes { get; set; }

            public decimal Discounts { get; set; }

            public decimal Total { get; set; }

            public DateTime IssuedAt { get; set; }

            public Booking Booking { get; set; }
        }
    }
}
