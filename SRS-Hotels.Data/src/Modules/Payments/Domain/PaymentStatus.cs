namespace SRS_Hotels.Data.src.Modules.Payments.Domain
{
    namespace SRS_Hotels.Modules.Payments.Domain
    {
        public enum PaymentStatus
        {
            Pending,
            Completed,
            Failed,
            Refunded,
            PartiallyRefunded
        }
    }
}
