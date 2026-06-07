namespace SRS_Hotels.Data.src.Modules.Bookings.Domain
{
    public class CheckIn
    {
        public Guid Id { get; set; }

        public Guid BookingId { get; set; }

        public DateTime CheckInTime { get; set; }

        public Guid EmployeeId { get; set; }

        public string? Notes { get; set; }
    }
}
