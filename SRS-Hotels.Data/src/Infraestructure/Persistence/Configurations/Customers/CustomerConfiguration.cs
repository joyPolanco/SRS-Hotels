using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SRS_Hotels.Data.src.Modules.Customers.Domain;

namespace SRS_Hotels.Data.src.Infraestructure.Persistence.Configurations.Customers
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FullName).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(150).IsRequired();
            builder.Property(x => x.Phone).HasMaxLength(20).IsRequired();

            builder.OwnsOne(x => x.Address, a =>
            {
                a.Property(p => p.Street).HasMaxLength(200);
                a.Property(p => p.City).HasMaxLength(100);
                a.Property(p => p.Country).HasMaxLength(100);
            });

            builder.OwnsOne(x => x.EmergencyContact);

            builder.HasOne(x => x.LoyaltyAccount)
                .WithOne(x => x.Customer)
                .HasForeignKey<LoyaltyAccount>(x => x.CustomerId);
        }
    }
}
