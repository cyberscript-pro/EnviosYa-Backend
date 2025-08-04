using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnviosYa.Infrastructure.Persistence.EntityConfiguration;

public class ProductReviews : IEntityTypeConfiguration<Domain.Entities.ProductReviews>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.ProductReviews> builder)
    {
        builder.ToTable("ProductReviews");
        
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasColumnType("uuid");
        
        builder.Property(r => r.Rating)
            .HasDefaultValue(1);
        
        builder.Property(r => r.Comment)
            .HasMaxLength(500);

        builder.Property(r => r.CreatedAt)
            .IsRequired();

        builder.Property(r => r.IsAvailable)
            .HasDefaultValue(true);
        
        builder.HasOne(r => r.Product)
            .WithMany(p => p.Reviews)
            .HasForeignKey(r => r.ProductId)
            .OnDelete(DeleteBehavior.Cascade);;
        
        builder.HasOne(r => r.User)
            .WithMany(p => p.ProductReviews)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}