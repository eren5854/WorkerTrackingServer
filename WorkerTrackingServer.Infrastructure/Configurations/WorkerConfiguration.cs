using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkerTrackingServer.Domain.Enums;
using WorkerTrackingServer.Domain.Workers;

namespace WorkerTrackingServer.Infrastructure.Configurations;
public sealed class WorkerConfiguration : IEntityTypeConfiguration<Worker>
{
    public void Configure(EntityTypeBuilder<Worker> builder)
    {
        builder
            .Property(p => p.Gender)
            .HasConversion(p => p.Value,
            v => GenderSmartEnum.FromValue(v));

        builder
            .Property(p => p.FirstName)
            .HasColumnType("varchar(150)")
            .HasMaxLength(150)
            .IsRequired();

        builder
            .Property(p => p.LastName)
            .HasColumnType("varchar(250)")
            .HasMaxLength(150)
            .IsRequired();

        builder
            .Property(p => p.WorkerCode)
            .HasColumnType("varchar(50)")
            .HasMaxLength(50)
            .IsRequired();

        builder.HasQueryFilter(filter => !filter.IsDeleted);
    }
}
