using EnviosYa.Domain.Constants;
using EnviosYa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnviosYa.Infrastructure.Persistence.EntityConfiguration;

public class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasColumnType("uuid");

        builder.Property(p => p.DiscountPrice)
            .IsRequired();
        
        builder.Property(p => p.Price)
            .IsRequired();

        builder.Property(p => p.Stock)
            .IsRequired();
        
        builder.Property(p => p.ProductImages)
            .HasColumnType("text[]")
            .IsRequired();
        
        builder.Property(rt => rt.CreatedAt)
            .IsRequired();
        
        builder.Property(rt => rt.UpdatedAt)
            .IsRequired();
        
        builder.Property(p => p.IsAvailable)
            .HasDefaultValue(true);

        builder.HasOne(p => p.Category)
            .WithMany(ca => ca.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.Translations)
            .WithOne(t => t.Product)
            .HasForeignKey(t => t.ProductId);
        
        builder.HasMany(p => p.Specifications)
            .WithOne(s => s.Product)
            .HasForeignKey(s => s.ProductId);
        
        builder.HasMany(p => p.Tags)
            .WithOne(ta => ta.Product)
            .HasForeignKey(ta => ta.ProductId);
        
        builder.HasMany(p => p.Reviews)
            .WithOne(rev => rev.Product)
            .HasForeignKey(rev => rev.ProductId);
    }
}