using HRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRM.Persistance.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Departments").HasKey(d => d.Id);
            builder.Property(d => d.Name).IsRequired().HasMaxLength(100);
            builder.HasData(
                new Department
                {
                    Id = Guid.Parse("6cfa24f9-bdea-4f69-9282-fc5d17eda487"),
                    Name = "HR"
                },
                new Department
                {
                    Id = Guid.Parse("5098fb31-448e-4fc8-bba1-74752324d5a7"),
                    Name = "Software Development"
                },
                new Department
                {
                    Id = Guid.Parse("e20cfeef-74b7-47b9-b47d-84fa07c85caa"),
                    Name = "Finance"
                },
                new Department
                {
                    Id = Guid.Parse("f2a0b1c3-4d5e-4c8b-9f6d-7a1e2f3c4b5a"),
                    Name = "Accountant"
                }
            );
        }
    }
}
