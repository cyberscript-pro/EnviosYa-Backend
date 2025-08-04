using EnviosYa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnviosYa.Infrastructure.Persistence.EntityConfiguration;

public class ProductTagsTranslationsEntityConfiguration : IEntityTypeConfiguration<ProductTagsTranslations>
{
    public void Configure(EntityTypeBuilder<ProductTagsTranslations> builder)
    {
        builder.ToTable("ProductTagTranslations");

        builder.HasKey(tt => tt.Id);

        builder.Property(tt => tt.Id)
            .HasColumnType("uuid");

        builder.Property(tt => tt.Name)
            .IsRequired();

        builder.Property(tt => tt.IsAvailable)
            .HasDefaultValue(true);
        
        builder.HasOne(tt => tt.ProductTags)
            .WithMany(t => t.Translations)
            .HasForeignKey(tt => tt.ProductTagsId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(tt => tt.Language)
            .WithMany(l => l.ProductTagsTranslations)
            .HasForeignKey(tt => tt.LanguageId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}