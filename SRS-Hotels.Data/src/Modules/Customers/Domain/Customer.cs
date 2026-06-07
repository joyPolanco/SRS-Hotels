using System.Net;

namespace SRS_Hotels.Data.src.Modules.Customers.Domain
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;

        public Address Address { get; set; } = null!;
        public EmergencyContact EmergencyContact { get; set; } = null!;

        public LoyaltyAccount LoyaltyAccount { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
    }
}
