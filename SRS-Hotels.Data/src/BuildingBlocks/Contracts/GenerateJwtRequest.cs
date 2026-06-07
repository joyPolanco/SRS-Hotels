namespace SRS_Hotels.Data.src.BuildingBlocks.Contracts
{
    namespace SRS_Hotels.Data.src.BuildingBlocks.Contracts
    {
        public class GenerateJwtRequest
        {
            public Guid UserId { get; set; }

            public string Email { get; set; } = null!;

            public string FullName { get; set; } = null!;

            // Autorización
            public List<string> Roles { get; set; } = new();

            // Claims opcionales del sistema
            public Dictionary<string, string>? Claims { get; set; }

            // Control de seguridad / contexto
            public string? IpAddress { get; set; }

            public string? UserAgent { get; set; }
        }
    }
}
