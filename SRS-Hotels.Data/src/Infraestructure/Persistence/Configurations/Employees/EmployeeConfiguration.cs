using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SRS_Hotels.Data.src.Modules.Employees.Domain;

namespace SRS_Hotels.Data.src.Infraestructure.Persistence.Configurations.Employees
{
   
        public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
        {
            public void Configure(EntityTypeBuilder<Employee> builder)
            {
                builder.ToTable("Employees");

                builder.HasKey(x => x.Id);

                builder.Property(x => x.Status).HasConversion<string>();

                builder.HasOne<Position>().WithMany();
                builder.HasOne<WorkShift>().WithMany();
            }
        
    }
}
