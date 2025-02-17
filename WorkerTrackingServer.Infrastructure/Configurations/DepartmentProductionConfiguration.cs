using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkerTrackingServer.Domain.Departments;

namespace WorkerTrackingServer.Infrastructure.Configurations;
public sealed class DepartmentProductionConfiguration : IEntityTypeConfiguration<DepartmentProduction>
{
    public void Configure(EntityTypeBuilder<DepartmentProduction> builder)
    {
        builder.ToTable("DepartmentProductions");
        
        builder.HasQueryFilter(filter => !filter.IsDeleted);
    }
}
