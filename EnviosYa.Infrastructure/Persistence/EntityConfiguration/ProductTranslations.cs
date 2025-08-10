using EnviosYa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnviosYa.Infrastructure.Persistence.EntityConfiguration;

public class ProductTranslationsEntityConfiguration : IEntityTypeConfiguration<ProductTranslations>
{
    public void Configure(EntityTypeBuilder<ProductTranslations> builder)
    {
        builder.ToTable("ProductTranslations");
        
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .HasColumnType("uuid");

        builder.Property(t => t.Name)
            .IsRequired();

        builder.Property(t => t.Description);
        
        builder.Property(t => t.IsAvailable)
            .HasDefaultValue(true);
        
        builder.HasOne(t => t.Product)
            .WithMany(t => t.Translations)
            .HasForeignKey(t => t.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(t => t.Language)
            .WithMany(l => l.ProductTranslations)
            .HasForeignKey(t => t.LanguageId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}