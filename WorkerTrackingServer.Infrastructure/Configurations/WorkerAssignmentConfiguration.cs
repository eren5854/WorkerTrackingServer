using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkerTrackingServer.Domain.WorkerAssignments;

namespace WorkerTrackingServer.Infrastructure.Configurations;
public sealed class WorkerAssignmentConfiguration : IEntityTypeConfiguration<WorkerAssignment>
{
    public void Configure(EntityTypeBuilder<WorkerAssignment> builder)
    {
        builder.HasQueryFilter(filter => !filter.IsDeleted);
    }
}
