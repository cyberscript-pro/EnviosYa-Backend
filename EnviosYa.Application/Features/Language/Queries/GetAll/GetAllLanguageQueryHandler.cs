using EnviosYa.Application.Common;
using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace EnviosYa.Application.Features.Language.Queries.GetAll;

public class GetAllLanguageQueryHandler(IRepository repository) : IQueryHandler<GetAllLanguageQuery, List<GetAllLanguageResponseDto>>
{
    public async Task<Result<List<GetAllLanguageResponseDto>>> Handle(GetAllLanguageQuery query, CancellationToken cancellationToken = default)
    {
        var languages = await repository.Languages.Select(l => new GetAllLanguageResponseDto(
            l.Id.ToString(),
            l.Code,
            l.Name,
            l.CategoryTranslations.Select(ct => new Translation(ct.Name))
        )).ToListAsync(cancellationToken);

        return await Task.FromResult(Result.Success(languages));
    }
}