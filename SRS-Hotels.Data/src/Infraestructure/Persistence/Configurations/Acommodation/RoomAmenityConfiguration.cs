using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SRS_Hotels.Data.src.Modules.Accomodation.Domain;

namespace SRS_Hotels.Data.src.Infraestructure.Persistence.Configurations.Acommodation
{
    public class RoomAmenityConfiguration : IEntityTypeConfiguration<RoomAmenity>
    {
        public void Configure(EntityTypeBuilder<RoomAmenity> builder)
        {
            builder.ToTable("RoomAmenities");

            builder.HasKey(x => new { x.RoomId, x.AmenityId });

            builder.HasOne(x => x.Room)
                .WithMany(x => x.Amenities)
                .HasForeignKey(x => x.RoomId);

            builder.HasOne(x => x.Amenity)
                .WithMany()
                .HasForeignKey(x => x.AmenityId);
        }
    }
}
