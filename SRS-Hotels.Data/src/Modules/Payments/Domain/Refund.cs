using SRS_Hotels.Modules.Payments.Domain;

namespace SRS_Hotels.Data.src.Modules.Payments.Domain
{
    namespace SRS_Hotels.Modules.Payments.Domain
    {
        public class Refund
        {
            public Guid Id { get; set; }

            public Guid PaymentId { get; set; }

            public decimal Amount { get; set; }

            public string Reason { get; set; }

            public DateTime CreatedAt { get; set; }

            public Payment Payment { get; set; }
        }
    }
}
