using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SRS_Hotels.Data.src.Modules.Bookings.Domain;

namespace SRS_Hotels.Data.src.Infraestructure.Persistence.Configurations.Bookings
{
    public class PromotionUsageConfiguration : IEntityTypeConfiguration<PromotionUsage>
    {
        public void Configure(EntityTypeBuilder<PromotionUsage> builder)
        {
            builder.ToTable("PromotionUsages");

            builder.HasKey(x => x.Id);

            builder.HasOne<Promotion>()
                .WithMany()
                .HasForeignKey(x => x.PromotionId);

            builder.HasOne<Booking>()
                .WithMany()
                .HasForeignKey(x => x.BookingId);
        }
    }
}
