using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SRS_Hotels.Data.src.Modules.Accomodation.Domain;

namespace SRS_Hotels.Data.src.Infraestructure.Persistence.Configurations.Acommodation
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.ToTable("Rooms");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Number)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.Status)
                .HasConversion<string>();

            builder.HasOne<Floor>()
                .WithMany(x => x.Rooms)
                .HasForeignKey(x => x.FloorId);

            builder.HasOne<RoomCategory>()
                .WithMany()
                .HasForeignKey(x => x.RoomCategoryId);

            builder.HasMany(x => x.Images)
                .WithOne()
                .HasForeignKey(x => x.RoomId);

            builder.HasMany(x => x.Amenities)
                .WithOne()
                .HasForeignKey(x => x.RoomId);

            builder.HasMany(x => x.Services)
                .WithOne()
                .HasForeignKey(x => x.RoomId);

            builder.HasMany(x => x.SeasonalPrices)
                .WithOne()
                .HasForeignKey(x => x.RoomId);
        }
    }
}
