using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SRS_Hotels.Data.src.Modules.Accomodation.Domain;

namespace SRS_Hotels.Data.src.Infraestructure.Persistence.Configurations.Acommodation
{
    public class SeasonalPriceConfiguration : IEntityTypeConfiguration<SeasonalPrice>
    {
        public void Configure(EntityTypeBuilder<SeasonalPrice> builder)
        {
            builder.ToTable("SeasonalPrices");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Price)
                .HasColumnType("decimal(18,2)");

            builder.HasOne<Room>()
                .WithMany(x => x.SeasonalPrices)
                .HasForeignKey(x => x.RoomId);
        }
    }
}
