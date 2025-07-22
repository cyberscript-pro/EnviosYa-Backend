using EnviosYa.Domain.Entities;

namespace EnviosYa.Application.Common.Abstractions;

public interface IProductRepository
{
    Task AddAsync(Product product, CancellationToken cancellationToken = default);
}