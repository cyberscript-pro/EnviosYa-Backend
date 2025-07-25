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

        builder.Property(p => p.Name)
            .IsRequired();

        builder.Property(p => p.Description);
        
        builder.Property(p => p.Price)
            .IsRequired();

        builder.Property(p => p.Stock)
            .IsRequired();

        builder.Property(p => p.Category)
            .HasConversion<string>()
            .IsRequired();
        
        builder.Property(p => p.ImagesUrls)
            .HasColumnType("text[]")
            .IsRequired();
        
        builder.Property(p => p.IsAvailable)
            .HasDefaultValue(true);

    }
}