using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SRS_Hotels.Data.src.Modules.Accomodation.Domain;
using SRS_Hotels.Data.src.Modules.Bookings.Domain;
using SRS_Hotels.Data.src.Modules.Customers.Domain;

namespace SRS_Hotels.Data.src.Infraestructure.Persistence.Configurations.Bookings
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.ToTable("Bookings");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Status)
                .HasConversion<string>();

            builder.Property(x => x.BaseAmount).HasColumnType("decimal(18,2)");
            builder.Property(x => x.TotalAmount).HasColumnType("decimal(18,2)");

            builder.HasOne<Customer>()
                .WithMany()
                .HasForeignKey(x => x.CustomerId);

            builder.HasOne<Room>()
                .WithMany()
                .HasForeignKey(x => x.RoomId);

            builder.HasOne<Promotion>()
                .WithMany()
                .HasForeignKey(x => x.PromotionId);

            builder.HasOne<CancellationPolicy>()
                .WithMany()
                .HasForeignKey(x => x.CancellationPolicyId);

            builder.HasMany(x => x.Guests)
                .WithOne()
                .HasForeignKey(x => x.BookingId);

            builder.HasMany(x => x.History)
                .WithOne()
                .HasForeignKey(x => x.BookingId);
        }
    }
}
