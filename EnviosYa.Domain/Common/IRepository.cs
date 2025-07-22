using EnviosYa.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnviosYa.Domain.Common;

public interface IRepository
{
    public DbSet<User> Users { get; set; }
    
    Task<int> SaveChangesAsync (CancellationToken cancellationToken = default);
}