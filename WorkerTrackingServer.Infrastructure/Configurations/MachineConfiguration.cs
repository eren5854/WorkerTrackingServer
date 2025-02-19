using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkerTrackingServer.Domain.Machines;

namespace WorkerTrackingServer.Infrastructure.Configurations;
public sealed class MachineConfiguration : IEntityTypeConfiguration<Machine>
{
    public void Configure(EntityTypeBuilder<Machine> builder)
    {
        builder
            .Property(p => p.MachineName)
            .HasColumnType("varchar(500)")
            .HasMaxLength(500)
            .IsRequired();

        builder
            .Property(p => p.MachineBrand)
            .HasColumnType("varchar(500)")
            .HasMaxLength(500);

        builder
            .Property(p => p.MachineModel)
            .HasColumnType("varchar(500)")
            .HasMaxLength(500);

        builder
            .Property(p => p.MachineType)
            .HasColumnType("varchar(500)")
            .HasMaxLength(500);

        builder
            .Property(p => p.MachineLocation)
            .HasColumnType("varchar(500)")
            .HasMaxLength(500);

        builder
            .Property(p => p.MachineStatus)
            .HasColumnType("varchar(500)")
            .HasMaxLength(500);

        builder
            .Property(p => p.MachineDescription)
            .HasColumnType("varchar(1500)")
            .HasMaxLength(500);

        builder.HasQueryFilter(filter => !filter.IsDeleted);
    }
}
