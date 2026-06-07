using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SRS_Hotels.Data.src.Infraestructure.Identity.Entities;
using SRS_Hotels.Data.src.Modules.Authentication.Domain;

namespace SRS_Hotels.Data.src.Infraestructure.Persistence.Configurations.Authentication
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("RefreshTokens");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Token).HasMaxLength(300);

            builder.HasOne<ApplicationUser>()
                .WithMany(x => x.RefreshTokens)
                .HasForeignKey(x => x.UserId);
        }
    }
}
