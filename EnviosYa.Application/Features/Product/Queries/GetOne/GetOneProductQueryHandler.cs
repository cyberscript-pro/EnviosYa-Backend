using EnviosYa.Application.Common;
using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Domain.Common;

namespace EnviosYa.Application.Features.Product.Queries.GetOne;

public class GetOneProductQueryHandler(IRepository repository) : IQueryHandler<GetOneProductQuery, GetOneProductResponseDto>
{
    public async Task<Result<GetOneProductResponseDto>> Handle(GetOneProductQuery query, CancellationToken cancellationToken = default)
    {
        var product = await repository.Products.FindAsync(query.Id);

        if (product is null)
        {
            return await Task.FromResult(Result.Failure<GetOneProductResponseDto>(Error.Failure("400", "Product not found")));
        }

        return await Task.FromResult(
            Result.Success(new GetOneProductResponseDto(
                product.Id,
                product.Name,
                product.Description,
                product.Price,
                product.Stock,
                product.Category,
                product.ImagesUrls
            )
        ));
    }
}