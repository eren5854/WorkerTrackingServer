using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkerTrackingServer.Domain.Departments;

namespace WorkerTrackingServer.Infrastructure.Configurations;
public sealed class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder
            .Property(p => p.DepartmentName)
            .HasColumnType("varchar(250)")
            .HasMaxLength(250)
            .IsRequired();
        builder
            .Property(p => p.DepartmentDescription)
            .HasColumnType("varchar(MAX)");

        
        builder.HasQueryFilter(filter => !filter.IsDeleted);
    }
}
