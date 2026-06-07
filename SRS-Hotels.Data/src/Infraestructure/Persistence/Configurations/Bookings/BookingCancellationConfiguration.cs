using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SRS_Hotels.Data.src.Modules.Bookings.Domain;

namespace SRS_Hotels.Data.src.Infraestructure.Persistence.Configurations.Bookings
{
    public class BookingCancellationConfiguration : IEntityTypeConfiguration<BookingCancellation>
    {
        public void Configure(EntityTypeBuilder<BookingCancellation> builder)
        {
            builder.ToTable("BookingCancellations");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Reason).HasMaxLength(300);

            builder.Property(x => x.RefundAmount).HasColumnType("decimal(18,2)");
            builder.Property(x => x.PenaltyAmount).HasColumnType("decimal(18,2)");

            builder.HasOne<Booking>()
                .WithMany()
                .HasForeignKey(x => x.BookingId);
        }
    }
}
