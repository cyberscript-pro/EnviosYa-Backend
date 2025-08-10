using EnviosYa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnviosYa.Infrastructure.Persistence.EntityConfiguration;

public class ProductSpecificationsEntityConfiguration : IEntityTypeConfiguration<ProductSpecification>
{
    public void Configure(EntityTypeBuilder<ProductSpecification> builder)
    {
        builder.ToTable("ProductSpecifications");
        
        builder.HasKey(ps => ps.Id);
        
        builder.Property(ps => ps.Id)
            .HasColumnType("uuid");
        
        builder.Property(ps => ps.IsAvailable)
            .HasDefaultValue(true);
        
        builder.HasOne(ps => ps.Product)
            .WithMany(p => p.Specifications)
            .HasForeignKey(ps => ps.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(ps => ps.Translations)
            .WithOne(t => t.ProductSpecification)
            .HasForeignKey(t => t.ProductSpecificationId);
    }
}