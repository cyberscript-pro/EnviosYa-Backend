using EnviosYa.Domain.Common;
using EnviosYa.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnviosYa.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options), IRepository
{
    public DbSet<User> Users { get; set; }
    public DbSet<CategoryTranslations> CategoryTranslations { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductTranslations> ProductTranslations { get; set; }
    public DbSet<ProductSpecification> ProductSpecifications { get; set; }
    public DbSet<ProductSpecificationTranslations> ProductSpecificationTranslations { get; set; }
    public DbSet<ProductTags> ProductTags { get; set; }
    public DbSet<ProductTagsTranslations> ProductTagsTranslations { get; set; }
    public DbSet<ProductReviews> ProductReviews { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<User>()
            .HasOne(u => u.Cart)
            .WithOne(c => c.User)
            .HasForeignKey<Cart>(c => c.UserId);
        
        modelBuilder.Entity<Cart>()
            .HasMany(c => c.Items)
            .WithOne(i => i.Cart)
            .HasForeignKey(i => i.CartId);
    }
}