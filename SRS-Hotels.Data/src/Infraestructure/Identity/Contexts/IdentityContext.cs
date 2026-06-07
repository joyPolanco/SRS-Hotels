using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SRS_Hotels.Data.src.Infraestructure.Identity.Entities;
using SRS_Hotels.Data.src.Modules.Authentication.Domain;

namespace SRS_Hotels.Data.src.Infraestructure.Identity.Contexts
{
    public class IdentityContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityContext).Assembly);
        }

        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    }
}
