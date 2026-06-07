namespace SRS_Hotels.Data.src.Modules.Bookings.Domain
{
    public class CancellationRule
    {
        public Guid Id { get; set; }

        public Guid CancellationPolicyId { get; set; }

        public int HoursBeforeCheckIn { get; set; }

        public decimal RefundPercentage { get; set; }

        public decimal PenaltyPercentage { get; set; }

        public bool NonRefundable { get; set; }
    }
}
