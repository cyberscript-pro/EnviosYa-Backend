using EnviosYa.Application.Common;
using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace EnviosYa.Application.Features.ProductTranslations.Command.Create;

public class CreateProductTranslationsCommandHandler(IRepository repository) : ICommandHandler<CreateProductTranslationsCommand, CreateProductTranslationsResponseDto>
{
    public async Task<Result<CreateProductTranslationsResponseDto>> Handle(CreateProductTranslationsCommand command, CancellationToken cancellationToken = default)
    {
        var product = await repository.Products.Include(p => p.Translations).FirstOrDefaultAsync(p => p.Id == command.ProductId && p.IsAvailable, cancellationToken);

        if (product is null)
        {
            return await Task.FromResult(Result.Failure<CreateProductTranslationsResponseDto>(Error.Conflict("400", "Product not found")));
        }

        if (product.Translations.Any(p => p.LanguageId == command.LanguageId))
        {
            return await Task.FromResult(Result.Failure<CreateProductTranslationsResponseDto>(Error.Conflict("400", "Product already has this language translated")));
        }

        var productTranslation = new Domain.Entities.ProductTranslations
        {
            LanguageId = command.LanguageId,
            ProductId = command.ProductId,
            Name = command.Name,
            Description = command.Description,
        };
        
        repository.ProductTranslations.Add(productTranslation);
        await repository.SaveChangesAsync(cancellationToken);
        
        return await Task.FromResult(
            Result.Success(new CreateProductTranslationsResponseDto(productTranslation.Name))
        );
    }
}