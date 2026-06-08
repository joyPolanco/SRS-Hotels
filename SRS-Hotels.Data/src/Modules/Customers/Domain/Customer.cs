using System.Net;

namespace SRS_Hotels.Data.src.Modules.Customers.Domain
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;

        public Address? Address { get; set; }
        public EmergencyContact? EmergencyContact { get; set; }

        public LoyaltyAccount LoyaltyAccount { get; set; } = new LoyaltyAccount();

        public DateTime CreatedAt { get; set; }
    }
}
