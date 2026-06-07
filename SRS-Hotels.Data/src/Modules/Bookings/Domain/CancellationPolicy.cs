namespace SRS_Hotels.Data.src.Modules.Bookings.Domain
{
    public class CancellationPolicy
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public bool IsActive { get; set; }

        public ICollection<CancellationRule> Rules { get; set; } = new List<CancellationRule>();
    }
}
