using EnviosYa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnviosYa.Infrastructure.Persistence.EntityConfiguration;

public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Category");
        
        builder.HasKey(ca => ca.Id);

        builder.Property(ca => ca.Id)
            .HasColumnType("uuid");
        
        builder.HasMany(ca => ca.Products)
            .WithOne(p => p.Category)
            .HasForeignKey(p => p.CategoryId);
        
        builder.HasMany(ca => ca.CategoryTranslations)
            .WithOne(c => c.Category)
            .HasForeignKey(c => c.CategoryId);
    }
}