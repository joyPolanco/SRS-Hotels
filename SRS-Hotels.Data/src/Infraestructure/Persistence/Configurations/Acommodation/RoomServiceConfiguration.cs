using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SRS_Hotels.Data.src.Modules.Accomodation.Domain;

namespace SRS_Hotels.Data.src.Infraestructure.Persistence.Configurations.Acommodation
{
    public class RoomServiceConfiguration : IEntityTypeConfiguration<RoomService>
    {
        public void Configure(EntityTypeBuilder<RoomService> builder)
        {
            builder.ToTable("RoomServices");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(100);

            builder.Property(x => x.Price)
                .HasColumnType("decimal(18,2)");

            builder.HasOne<Room>()
                .WithMany(x => x.Services)
                .HasForeignKey(x => x.RoomId);
        }
    }
}
