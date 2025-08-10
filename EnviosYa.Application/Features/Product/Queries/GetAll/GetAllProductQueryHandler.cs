using EnviosYa.Application.Common;
using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Domain.Common;
using EnviosYa.Domain.Constants;
using Microsoft.EntityFrameworkCore;

namespace EnviosYa.Application.Features.Product.Queries.GetAll;

public class GetAllProductQueryHandler(IRepository repository)
    : IQueryHandler<GetAllProductQuery, List<GetAllProductResponseDto>>
{
    public async Task<Result<List<GetAllProductResponseDto>>> Handle(GetAllProductQuery query,
        CancellationToken cancellationToken = default)
    {
        var products = await repository.Products
            .Where(p =>
                p.Translations.Any(t =>
                    t.Language.Code == query.Language &&
                    t.Name          != null           &&
                    t.Description   != null
                ) &&
                p.Category.CategoryTranslations.First(t => t.Language.Code == query.Language).Name != null
            )
            .Select(p => new GetAllProductResponseDto(
                p.Id,
                p.Translations.First(t => t.Language.Code == query.Language).Name,
                p.Translations.First(t => t.Language.Code == query.Language).Description,
                p.Price,
                p.Stock,
                p.Category.CategoryTranslations.First(t => t.Language.Code == query.Language).Name,
                p.ProductImages.ToList()
            ))
            .ToListAsync(cancellationToken);

        return await Task.FromResult(Result.Success(products));
    }
}