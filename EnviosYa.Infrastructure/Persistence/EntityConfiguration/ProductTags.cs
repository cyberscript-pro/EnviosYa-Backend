using EnviosYa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnviosYa.Infrastructure.Persistence.EntityConfiguration;

public class ProductTagsEntityConfiguration : IEntityTypeConfiguration<ProductTags>
{
    public void Configure(EntityTypeBuilder<ProductTags> builder)
    {
        builder.ToTable("ProductTags");
        
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .HasColumnType("uuid");
        
        builder.Property(t => t.IsAvailable)
            .HasDefaultValue(true);

        builder.HasOne(t => t.Product)
            .WithMany(t => t.Tags)
            .HasForeignKey(t => t.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(t => t.Translations)
            .WithOne(t => t.ProductTags)
            .HasForeignKey(t => t.ProductTagsId);
    }
}