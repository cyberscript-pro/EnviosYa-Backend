using EnviosYa.Application.Common;
using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Domain.Common;

namespace EnviosYa.Application.Features.Product.Commands.Update;

internal sealed class UpdateProductCommandHandler(IRepository repository) : ICommandHandler<UpdateProductCommand, UpdateProductResponseDto>
{
    public async Task<Result<UpdateProductResponseDto>> Handle(UpdateProductCommand command, CancellationToken cancellationToken = default)
    {
        var product = await repository.Products.FindAsync(command.Id);

        if (product is null)
        {
            return await Task.FromResult(
                Result.Failure<UpdateProductResponseDto>(Error.NotFound("404", $"Product Not Found {command.Id}"))
            );
        }
        
        product.Name = command.Name;
        product.Description = command.Description;
        product.Price = command.Price;
        product.Stock = command.Stock;
        product.Category = command.Category;
        product.ImagesUrls = command.ImagesUrls;

        await repository.SaveChangesAsync(cancellationToken);

        return await Task.FromResult(
            Result.Success(new UpdateProductResponseDto(
                product.Id,
                product.Name,
                product.Description,
                product.Price,
                product.Stock,
                product.Category,
                product.ImagesUrls
            ))
        );
    }
}