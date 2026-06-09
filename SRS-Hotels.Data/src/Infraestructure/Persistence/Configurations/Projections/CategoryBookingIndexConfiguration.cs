using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SRS_Hotels.Data.src.Modules.Shared.Projections.SRS_Hotels.Data.src.Projections;

namespace SRS_Hotels.Data.src.Infraestructure.Persistence.Configurations.Projections
{
    public class CategoryBookingIndexConfiguration
          : IEntityTypeConfiguration<CategoryBookingIndex>
    {
        public void Configure(EntityTypeBuilder<CategoryBookingIndex> builder)
        {
            builder.ToTable("CategoryBookingIndex");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.CategoryId)
                .IsRequired();

            builder.Property(x => x.HasBookings)
                .IsRequired();

            builder.Property(x => x.TotalBookings)
                .IsRequired();

            builder.Property(x => x.UpdatedAt)
                .IsRequired();

            builder.HasIndex(x => x.CategoryId)
                .IsUnique();
        }
    }
}
