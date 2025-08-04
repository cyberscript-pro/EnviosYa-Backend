using EnviosYa.Application.Common;
using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace EnviosYa.Application.Features.Product.Queries.GetOne;

public class GetOneProductQueryHandler(IRepository repository) : IQueryHandler<GetOneProductQuery, GetOneProductResponseDto>
{
    public async Task<Result<GetOneProductResponseDto>> Handle(GetOneProductQuery query, CancellationToken cancellationToken = default)
    {
        var product = await repository.Products.FirstOrDefaultAsync(p => p.Id == query.Id && p.IsAvailable, cancellationToken);

        if (product is null)
        {
            return await Task.FromResult(Result.Failure<GetOneProductResponseDto>(Error.Failure("400", "Product not found")));
        }

        return await Task.FromResult(
            Result.Success(GetOneProductToResponse.MapToResponse(product)
        ));
    }
}