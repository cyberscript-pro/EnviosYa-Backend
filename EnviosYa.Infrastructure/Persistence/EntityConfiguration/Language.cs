using EnviosYa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnviosYa.Infrastructure.Persistence.EntityConfiguration;

public class LanguageEntityConfiguration : IEntityTypeConfiguration<Language>
{
    public void Configure(EntityTypeBuilder<Language> builder)
    {
        builder.ToTable("Language");
        
        builder.HasKey(l => l.Id);

        builder.Property(l => l.Id)
            .HasColumnType("uuid");

        builder.Property(l => l.Name)
            .IsRequired();
        
        builder.Property(l => l.Code)
            .IsRequired();
        
        builder.HasMany(l => l.ProductTranslations)
            .WithOne(p => p.Language)
            .HasForeignKey(p => p.LanguageId);
        
        builder.HasMany(l => l.ProductSpecificationTranslations)
            .WithOne(p => p.Language)
            .HasForeignKey(p => p.LanguageId);

        builder.HasMany(l => l.ProductTagsTranslations)
            .WithOne(p => p.Language)
            .HasForeignKey(p => p.LanguageId);
        
        builder.HasMany(l => l.CategoryTranslations)
            .WithOne(c => c.Language)
            .HasForeignKey(c => c.LanguageId);
    }
}