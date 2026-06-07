using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SRS_Hotels.Data.src.Modules.Bookings.Domain;

namespace SRS_Hotels.Data.src.Infraestructure.Persistence.Configurations.Bookings
{
    public class CheckInConfiguration : IEntityTypeConfiguration<CheckIn>
    {
        public void Configure(EntityTypeBuilder<CheckIn> builder)
        {
            builder.ToTable("CheckIns");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Notes).HasMaxLength(300);

            builder.HasOne<Booking>()
                .WithMany()
                .HasForeignKey(x => x.BookingId);
        }
    }
}
