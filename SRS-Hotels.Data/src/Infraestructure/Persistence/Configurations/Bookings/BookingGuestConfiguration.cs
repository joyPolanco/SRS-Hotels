using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SRS_Hotels.Data.src.Modules.Bookings.Domain;

namespace SRS_Hotels.Data.src.Infraestructure.Persistence.Configurations.Bookings
{
    public class BookingGuestConfiguration : IEntityTypeConfiguration<BookingGuest>
    {
        public void Configure(EntityTypeBuilder<BookingGuest> builder)
        {
            builder.ToTable("BookingGuests");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FullName).HasMaxLength(150);

            builder.HasOne<Booking>()
                .WithMany(x => x.Guests)
                .HasForeignKey(x => x.BookingId);
        }
    }
}
