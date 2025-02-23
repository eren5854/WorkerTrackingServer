using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkerTrackingServer.Domain.WorkerProductions;

namespace WorkerTrackingServer.Infrastructure.Configurations;
public sealed class WorkerProductionConfiguration : IEntityTypeConfiguration<WorkerProduction>
{
    public void Configure(EntityTypeBuilder<WorkerProduction> builder)
    {
        builder.HasQueryFilter(filter => !filter.IsDeleted);
    }
}
