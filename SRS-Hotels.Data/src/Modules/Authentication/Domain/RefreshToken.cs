namespace SRS_Hotels.Data.src.Modules.Authentication.Domain
{
    public class RefreshToken
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Token { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public DateTime ExpiresAt { get; set; }
        public Guid ReplacedById { get; set; }
        public DateTime? RevokedAt { get; set; }

        public bool IsActive => RevokedAt == null && DateTime.UtcNow < ExpiresAt;
        public bool IsUsed { get; set; }
        public Guid ReplacedByTokenId { get; internal set; }
    }
}
