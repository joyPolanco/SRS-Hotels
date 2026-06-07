using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SRS_Hotels.Data.src.Modules.Accomodation.Domain;

namespace SRS_Hotels.Data.src.Infraestructure.Persistence.Configurations.Acommodation
{
    public class RoomImageConfiguration : IEntityTypeConfiguration<RoomImage>
    {
        public void Configure(EntityTypeBuilder<RoomImage> builder)
        {
            builder.ToTable("RoomImages");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Url).HasMaxLength(500);

            builder.Property(x => x.IsMain)
                .HasDefaultValue(false);

            builder.HasOne<Room>()
                .WithMany(x => x.Images)
                .HasForeignKey(x => x.RoomId);
        }
    }
}
