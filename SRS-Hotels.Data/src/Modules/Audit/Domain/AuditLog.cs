namespace SRS_Hotels.Data.src.Modules.Audit.Domain
{
    public class AuditLog
    {
        public Guid Id { get; set; }

        public Guid? UserId { get; set; }

        public string EntityName { get; set; } = null!;
        public string EntityId { get; set; } = null!;

        public string Action { get; set; } = null!; // Create, Update, Delete

        public DateTime Timestamp { get; set; }

        public string OldValues { get; set; } = null!;
        public string NewValues { get; set; } = null!;

        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
    }
}
