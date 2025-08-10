using EnviosYa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnviosYa.Infrastructure.Persistence.EntityConfiguration;

public class CategoryTranslationsEntityConfiguration : IEntityTypeConfiguration<CategoryTranslations>
{
    public void Configure(EntityTypeBuilder<CategoryTranslations> builder)
    {
        builder.ToTable("CategoryTranslations");
        
        builder.HasKey(ct => ct.Id);

        builder.Property(ct => ct.Id)
            .HasColumnType("uuid");

        builder.Property(ca => ca.Name)
            .IsRequired();

        builder.Property(ca => ca.IsAvailable)
            .HasDefaultValue(true);
        
        builder.HasOne(ct => ct.Language)
            .WithMany(l => l.CategoryTranslations)
            .HasForeignKey(ct => ct.LanguageId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(ct => ct.Category)
            .WithMany(c => c.CategoryTranslations)
            .HasForeignKey(ct => ct.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}