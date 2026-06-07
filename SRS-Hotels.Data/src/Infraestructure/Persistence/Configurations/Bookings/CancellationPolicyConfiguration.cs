using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SRS_Hotels.Data.src.Modules.Bookings.Domain;

namespace SRS_Hotels.Data.src.Infraestructure.Persistence.Configurations.Bookings
{
    public class CancellationPolicyConfiguration : IEntityTypeConfiguration<CancellationPolicy>
    {
        public void Configure(EntityTypeBuilder<CancellationPolicy> builder)
        {
            builder.ToTable("CancellationPolicies");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(100);

            builder.HasMany(x => x.Rules)
                .WithOne()
                .HasForeignKey(x => x.CancellationPolicyId);
        }
    }
}
