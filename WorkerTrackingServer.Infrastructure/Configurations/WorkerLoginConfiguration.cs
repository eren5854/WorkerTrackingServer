using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkerTrackingServer.Domain.Workers;

namespace WorkerTrackingServer.Infrastructure.Configurations;
public sealed class WorkerLoginConfiguration : IEntityTypeConfiguration<WorkerLogin>
{
    public void Configure(EntityTypeBuilder<WorkerLogin> builder)
    {
        builder.HasQueryFilter(filter => !filter.IsDeleted);
    }
}
