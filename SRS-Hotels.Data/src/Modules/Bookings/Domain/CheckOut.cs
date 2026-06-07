namespace SRS_Hotels.Data.src.Modules.Bookings.Domain
{
    public class CheckOut
    {
        public Guid Id { get; set; }

        public Guid BookingId { get; set; }

        public DateTime CheckOutTime { get; set; }

        public decimal FinalAmount { get; set; }

        public decimal ExtraCharges { get; set; }

        public Guid EmployeeId { get; set; }

        public string? Notes { get; set; }
    }
}
