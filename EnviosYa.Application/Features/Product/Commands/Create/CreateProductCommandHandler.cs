using EnviosYa.Application.Common;
using Microsoft.EntityFrameworkCore;
using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Application.Features.ProductTranslations.Command.Create;
using EnviosYa.Domain.Common;

namespace EnviosYa.Application.Features.Product.Commands.Create;

public class CreateProductCommandHandler(IRepository repository) : ICommandHandler<CreateProductCommand, CreateProductResponseDto>
{
    public async Task<Result<CreateProductResponseDto>> Handle(CreateProductCommand command,
        CancellationToken cancellationToken = default)
    {
        var productExits = await repository.ProductTranslations.AnyAsync(p => p.Name == command.Name && p.IsAvailable, cancellationToken);
        
        if (productExits)
        {
            return await Task.FromResult(
                Result.Failure<CreateProductResponseDto>(Error.Conflict("400","Product already exists"))
            );
        }
        
        var product = new Domain.Entities.Product
        {
            DiscountPrice = command.DiscountPrice,
            Price = command.Price,
            Stock = command.Stock,
            CategoryId = command.CategoryId,
            ProductImages = command.ProductImages,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsAvailable = true
        };
        
        repository.Products.Add(product);
        await repository.SaveChangesAsync(cancellationToken);

        var commandTranslation = new CreateProductTranslationsCommand
        {
            ProductId = product.Id,
            Name = command.Name,
            Description = command.Description,
            LanguageId = command.LanguageId,
        };
        
        var handler = new CreateProductTranslationsCommandHandler(repository);
        var result = await handler.Handle(commandTranslation, cancellationToken);
        
        return await Task.FromResult(
            result.IsSuccess ? 
                Result.Success(new CreateProductResponseDto(product.Id.ToString())) 
                : Result.Failure<CreateProductResponseDto>(Error.Conflict("400", result.Error.ToString()))
        );
    }
}