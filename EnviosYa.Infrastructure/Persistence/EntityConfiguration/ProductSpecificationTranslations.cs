using EnviosYa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnviosYa.Infrastructure.Persistence.EntityConfiguration;

public class ProductSpecificationTranslationsEntityConfiguration : IEntityTypeConfiguration<ProductSpecificationTranslations>
{
    public void Configure(EntityTypeBuilder<ProductSpecificationTranslations> builder)
    {
        builder.ToTable("ProductSpecificationTranslations");

        builder.HasKey(pst => pst.Id);
        
        builder.Property(pst => pst.Id)
            .HasColumnType("uuid");

        builder.Property(pst => pst.Key)
            .IsRequired();
        
        builder.Property(pst => pst.Value)
            .IsRequired();
        
        builder.Property(pst => pst.IsAvailable)
            .HasDefaultValue(true);
        
        builder.HasOne(pst => pst.ProductSpecification)
            .WithMany(p => p.Translations)
            .HasForeignKey(pst => pst.ProductSpecificationId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(pst => pst.Language)
            .WithMany(l => l.ProductSpecificationTranslations)
            .HasForeignKey(pst => pst.LanguageId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}