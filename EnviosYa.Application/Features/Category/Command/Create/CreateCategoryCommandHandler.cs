using EnviosYa.Application.Common;
using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Application.Features.Category.Command.CreateTranslation;
using EnviosYa.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace EnviosYa.Application.Features.Category.Command.Create;

public class CreateCategoryCommandHandler(IRepository repository) : ICommandHandler<CreateCategoryCommand, CreateCategoryResponseDto>
{
    public async Task<Result<CreateCategoryResponseDto>> Handle(CreateCategoryCommand command, CancellationToken cancellationToken = default)
    {
        var categoryTranslationExists = await repository.CategoryTranslations.FirstOrDefaultAsync(x => x.Name == command.Name && x.IsAvailable, cancellationToken);

        if (categoryTranslationExists is not null)
        {
            return await Task.FromResult(Result.Failure<CreateCategoryResponseDto>(Error.Conflict("400", "Category already exists")));
        }

        var category = new Domain.Entities.Category();
        
        repository.Categories.Add(category);
        await repository.SaveChangesAsync(cancellationToken);

        var commandTranslation = new CreateCategoryTranslationsCommand
        {
            CategoryId = category.Id,
            LanguageId = command.LanguageId,
            Name = command.Name
        };
        
        var handler = new CreateCategoryTranslationsCommandHandler(repository);
        
        var result = await handler.Handle(commandTranslation, cancellationToken);
        
        return await Task.FromResult(
            result.IsSuccess ? 
                Result.Success(new CreateCategoryResponseDto(category.Id.ToString())) 
                : Result.Failure<CreateCategoryResponseDto>(Error.Conflict("400", result.Error.ToString()))
        );
    }
}