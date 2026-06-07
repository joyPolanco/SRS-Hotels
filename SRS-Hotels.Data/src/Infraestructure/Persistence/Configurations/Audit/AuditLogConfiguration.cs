using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SRS_Hotels.Data.src.Modules.Audit.Domain;

namespace SRS_Hotels.Data.src.Infraestructure.Persistence.Configurations.Audit
{
    public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.ToTable("AuditLogs");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.EntityName).HasMaxLength(100);
            builder.Property(x => x.EntityId).HasMaxLength(100);

            builder.Property(x => x.Action).HasMaxLength(50);

            builder.Property(x => x.OldValues).HasColumnType("nvarchar(max)");
            builder.Property(x => x.NewValues).HasColumnType("nvarchar(max)");

            builder.Property(x => x.IpAddress).HasMaxLength(45);
            builder.Property(x => x.UserAgent).HasMaxLength(300);
        }
    }
}
