namespace SRS_Hotels.Data.src.Modules.Shared.Projections
{
    namespace SRS_Hotels.Data.src.Projections
    {
        public class CategoryBookingIndex
        {
            public Guid Id { get; set; }

            public Guid CategoryId { get; set; }

            public bool HasBookings { get; set; }

            public int TotalBookings { get; set; }

            public DateTime UpdatedAt { get; set; }
        }
    }
}
