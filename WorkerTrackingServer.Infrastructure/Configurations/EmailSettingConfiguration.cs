using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkerTrackingServer.Domain.EmailSettings;

namespace WorkerTrackingServer.Infrastructure.Configurations;
public sealed class EmailSettingConfiguration : IEntityTypeConfiguration<EmailSetting>
{
    public void Configure(EntityTypeBuilder<EmailSetting> builder)
    {
        builder
            .Property(p => p.Email)
            .HasColumnType("varchar(200)")
            .HasMaxLength(200)
            .IsRequired();

        builder
            .Property(p => p.AppPassword)
            .HasColumnType("varchar(MAX)")
            .IsRequired();

        builder
            .Property(p => p.SmtpDomainName)
            .HasColumnType("varchar(200)")
            .HasMaxLength(200)
            .IsRequired();

        builder
             .HasQueryFilter(filter => !filter.IsDeleted);

    }
}
