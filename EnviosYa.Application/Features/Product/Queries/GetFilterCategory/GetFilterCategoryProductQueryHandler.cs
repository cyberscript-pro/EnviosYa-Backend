using EnviosYa.Application.Common;
using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace EnviosYa.Application.Features.Product.Queries.GetFilterCategory;

public class GetFilterCategoryProductQueryHandler(IRepository repository) : IQueryHandler<GetFilterCategoryProductQuery, List<GetFilterCategoryProductResponseDto>>
{
    public async Task<Result<List<GetFilterCategoryProductResponseDto>>> Handle(GetFilterCategoryProductQuery query, CancellationToken cancellationToken = default)
    {
        var products = await repository.Products.Where(p => p.IsAvailable && p.Category == query.Category)
            .OrderBy(p => p.Name).ToListAsync(cancellationToken);
        
        var response = products.Select(GetFilterCategoryProductToResponse.MapToResponse).ToList(); 
        
        return await Task.FromResult(Result.Success(response));
    }
}