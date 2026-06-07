using Microsoft.AspNetCore.Identity;
using SRS_Hotels.Data.src.Modules.Authentication.Domain;

namespace SRS_Hotels.Data.src.Infraestructure.Identity.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FullName { get; set; } = null!;
        public DateTime CreatedAt { get; set; }


        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

    }
}
