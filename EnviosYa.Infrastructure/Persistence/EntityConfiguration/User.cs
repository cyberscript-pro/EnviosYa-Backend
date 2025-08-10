using EnviosYa.Domain.Constants;
using EnviosYa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnviosYa.Infrastructure.Persistence.EntityConfiguration;

public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .HasColumnType("uuid");
        
        builder.Property(u => u.FullName)
            .IsRequired();

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(255);

        builder.HasIndex(u => u.Email).IsUnique();

        builder.Property(u => u.Password)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(u => u.Role)
            .HasDefaultValue(RolUser.Cliente)
            .HasMaxLength(50);

        builder.Property(u => u.Phone)
            .IsRequired();

        builder.Property(u => u.Cart);

        builder.Property(u => u.ProfilePicture);
        
        builder.Property(u => u.Provider)
            .HasDefaultValue(Provider.Credentials);

        builder.Property(u => u.ProviderId)
            .HasMaxLength(255);

        builder.Property(u => u.IsNewUser)
            .HasDefaultValue(true);
        
        builder.Property(u => u.IsAvailable)
            .HasDefaultValue(true);
        
        builder.Property(u => u.IsVerifiedEmail)
            .HasDefaultValue(false);
        
        builder.HasOne(u => u.Cart)
            .WithOne(c => c.User)
            .HasForeignKey<Cart>(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(u => u.RefreshTokens)
            .WithOne(rt => rt.User)
            .HasForeignKey(rt => rt.UserId);
        
        builder.HasMany(u => u.ProductReviews)
            .WithOne(r => r.User)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}