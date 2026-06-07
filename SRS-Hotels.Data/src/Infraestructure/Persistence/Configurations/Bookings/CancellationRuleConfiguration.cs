using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SRS_Hotels.Data.src.Modules.Bookings.Domain;

namespace SRS_Hotels.Data.src.Infraestructure.Persistence.Configurations.Bookings
{
    public class CancellationRuleConfiguration : IEntityTypeConfiguration<CancellationRule>
    {
        public void Configure(EntityTypeBuilder<CancellationRule> builder)
        {
            builder.ToTable("CancellationRules");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.RefundPercentage).HasColumnType("decimal(5,2)");
            builder.Property(x => x.PenaltyPercentage).HasColumnType("decimal(5,2)");
        }
    }
}
