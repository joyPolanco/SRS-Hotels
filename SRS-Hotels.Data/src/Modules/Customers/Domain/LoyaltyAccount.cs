namespace SRS_Hotels.Data.src.Modules.Customers.Domain
{
    public class LoyaltyAccount
    {
        public Guid Id { get; set; }
        public int Points { get; set; }
        public LoyaltyLevel Level { get; set; }

        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
    }
}
