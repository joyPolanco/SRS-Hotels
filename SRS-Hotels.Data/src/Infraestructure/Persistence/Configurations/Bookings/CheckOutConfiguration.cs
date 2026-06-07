using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SRS_Hotels.Data.src.Modules.Bookings.Domain;

namespace SRS_Hotels.Data.src.Infraestructure.Persistence.Configurations.Bookings
{
    public class CheckOutConfiguration : IEntityTypeConfiguration<CheckOut>
    {
        public void Configure(EntityTypeBuilder<CheckOut> builder)
        {
            builder.ToTable("CheckOuts");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FinalAmount).HasColumnType("decimal(18,2)");
            builder.Property(x => x.ExtraCharges).HasColumnType("decimal(18,2)");

            builder.HasOne<Booking>()
                .WithMany()
                .HasForeignKey(x => x.BookingId);
        }
    }
}
