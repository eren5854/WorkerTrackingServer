using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkerTrackingServer.Domain.Products;

namespace WorkerTrackingServer.Infrastructure.Configurations;
public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder
            .Property(p => p.ProductName)
            .HasColumnType("varchar(300)")
            .HasMaxLength(300)
            .IsRequired();

        builder
            .Property(p => p.ProductDescription)
            .HasColumnType("varchar(MAX)")
            .HasMaxLength(300);

        builder
            .Property(p => p.ProductCode)
            .HasColumnType("varchar(500)")
            .HasMaxLength(500)
            .IsRequired();

        builder.HasQueryFilter(filter => !filter.IsDeleted);
    }
}
