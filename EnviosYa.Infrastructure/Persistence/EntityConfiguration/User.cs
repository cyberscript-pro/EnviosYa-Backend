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

        builder.Property(u => u.Nickname)
            .IsRequired();

        builder.Property(u => u.Phone)
            .IsRequired();

        builder.Property(u => u.Email)
            .IsRequired();

        builder.Property(u => u.Password)
            .IsRequired();

        builder.Property(u => u.Role)
            .HasDefaultValue(RolUser.Cliente);

        builder.Property(u => u.ProfilePicture);

        builder.Property(u => u.IsAvailable)
            .HasDefaultValue(true);
        
        builder.HasOne(u => u.Cart)
            .WithOne(c => c.User)
            .HasForeignKey<Cart>(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}