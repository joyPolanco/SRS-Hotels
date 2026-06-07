namespace SRS_Hotels.Data.src.Modules.Bookings.Domain
{
    public class Promotion
    {
        public Guid Id { get; set; }

        public string Code { get; set; } = null!;
        public string Description { get; set; } = null!;

        public decimal DiscountPercentage { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; }
    }
}
