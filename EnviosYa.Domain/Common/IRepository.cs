using EnviosYa.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnviosYa.Domain.Common;

public interface IRepository
{
    public DbSet<User> Users { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<CategoryTranslations> CategoryTranslations { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductTranslations> ProductTranslations { get; set; }
    public DbSet<ProductSpecification>  ProductSpecifications { get; set; }
    public DbSet<ProductSpecificationTranslations> ProductSpecificationTranslations { get; set; }
    public DbSet<ProductTags> ProductTags { get; set; }
    public DbSet<ProductTagsTranslations> ProductTagsTranslations { get; set; }
    public DbSet<ProductReviews> ProductReviews { get; set; }
    
    Task<int> SaveChangesAsync (CancellationToken cancellationToken = default);
}