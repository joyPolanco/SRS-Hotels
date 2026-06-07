using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SRS_Hotels.Data.src.Modules.Bookings.Domain;

namespace SRS_Hotels.Data.src.Infraestructure.Persistence.Configurations.Bookings
{
    public class BookingHistoryConfiguration : IEntityTypeConfiguration<BookingHistory>
    {
        public void Configure(EntityTypeBuilder<BookingHistory> builder)
        {
            builder.ToTable("BookingHistory");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Action).HasMaxLength(100);
            builder.Property(x => x.Details).HasMaxLength(500);

            builder.HasOne<Booking>()
                .WithMany(x => x.History)
                .HasForeignKey(x => x.BookingId);
        }
    }
}
