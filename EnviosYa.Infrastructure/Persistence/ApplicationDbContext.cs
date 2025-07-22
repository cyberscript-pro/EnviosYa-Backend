using EnviosYa.Domain.Common;
using EnviosYa.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnviosYa.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options), IRepository
{
    public DbSet<User> Users { get; set; }
}