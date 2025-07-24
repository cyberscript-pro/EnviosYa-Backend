using EnviosYa.Application.Common;
using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace EnviosYa.Application.Features.Product.Queries.GetAll;

public class GetAllProductQueryHandler(IRepository repository) : IQueryHandler<GetAllProductQuery, List<GetAllProductResponseDto>>
{
    public async Task<Result<List<GetAllProductResponseDto>>> Handle(GetAllProductQuery query, CancellationToken cancellationToken = default)
    {
        var products = await repository.Products.Where(p => p.IsAvailable).ToListAsync(cancellationToken);

        var response = products.Select(GetAllProductToResponse.MapToResponse).ToList();

        return await Task.FromResult(Result.Success(response));
    }
    
}