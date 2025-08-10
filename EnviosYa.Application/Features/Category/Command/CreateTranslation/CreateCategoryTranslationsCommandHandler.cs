using EnviosYa.Application.Common;
using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace EnviosYa.Application.Features.Category.Command.CreateTranslation;

public class CreateCategoryTranslationsCommandHandler(IRepository repository) : ICommandHandler<CreateCategoryTranslationsCommand, CreateCategoryTranslationsResponseDto>
{
    public async Task<Result<CreateCategoryTranslationsResponseDto>> Handle(CreateCategoryTranslationsCommand command, CancellationToken cancellationToken = default)
    {
        var category = await repository.Categories.Include(ca => ca.CategoryTranslations).FirstOrDefaultAsync(ca => ca.Id == command.CategoryId, cancellationToken);

        if (category is null)
        {
            return await Task.FromResult(Result.Failure<CreateCategoryTranslationsResponseDto>(Error.Conflict("400", "Category not found")));
        }
        
        if (category.CategoryTranslations.Any(catTranslation => catTranslation.LanguageId == command.LanguageId))
        {
            return await Task.FromResult(Result.Failure<CreateCategoryTranslationsResponseDto>(Error.Conflict("400", "Category already has this language translated")));
        }
        
        var categoryTranslation = new Domain.Entities.CategoryTranslations
        {
            CategoryId = command.CategoryId,
            LanguageId = command.LanguageId,
            Name = command.Name,
        };
        
        repository.CategoryTranslations.Add(categoryTranslation);
        await repository.SaveChangesAsync(cancellationToken);

        return await Task.FromResult(
            Result.Success(new CreateCategoryTranslationsResponseDto(categoryTranslation.Name))
        );
    }
}