using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkerTrackingServer.Domain.Enums;
using WorkerTrackingServer.Domain.Users;

namespace WorkerTrackingServer.Infrastructure.Configurations;
public sealed class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder
            .Property(p => p.FirstName)
            .HasColumnType("varchar(150)")
            .HasMaxLength(150)
            .IsRequired();

        builder
            .Property(p => p.LastName)
            .HasColumnType("varchar(250)")
            .HasMaxLength(250)
            .IsRequired();

        builder
            .Property(p => p.Email)
            .HasColumnType("varchar(175)")
            .HasMaxLength(175)
            .IsRequired();

        builder
            .Property(p => p.Role)
            .HasConversion(p => p.Value,
                           v => UserRoleSmartEnum
                           .FromValue(v));

        builder.HasQueryFilter(filter => !filter.IsDeleted);
    }
}
