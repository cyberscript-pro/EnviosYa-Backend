using EnviosYa.Application.Common;
using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace EnviosYa.Application.Features.Category.Queries.GetAll;

public class GetAllCategoryQueryHandler(IRepository repository) : IQueryHandler<GetAllCategoryQuery, List<GetAllCategoryResponseDto>>
{
    public async Task<Result<List<GetAllCategoryResponseDto>>> Handle(GetAllCategoryQuery query, CancellationToken cancellationToken = default)
    {
        var categories = await repository.Categories.Select(ca => new GetAllCategoryResponseDto(
            ca.CategoryTranslations.Select(t => new Translation(t.Name, t.Language.Name)),
            ca.Id.ToString()
            )).ToListAsync(cancellationToken);
        
        return await Task.FromResult(Result.Success(categories));
    }
}