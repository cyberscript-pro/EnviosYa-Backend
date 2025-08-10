using EnviosYa.Application.Common;
using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace EnviosYa.Application.Features.Product.Commands.Update;

internal sealed class UpdateProductCommandHandler(IRepository repository) : ICommandHandler<UpdateProductCommand, UpdateProductResponseDto>
{
    public async Task<Result<UpdateProductResponseDto>> Handle(UpdateProductCommand command, CancellationToken cancellationToken = default)
    {
        var product = await repository.Products.FirstOrDefaultAsync(p => p.Id == command.Id && p.IsAvailable, cancellationToken);

        if (product is null)
        {
            return await Task.FromResult(
                Result.Failure<UpdateProductResponseDto>(Error.NotFound("400", $"Product Not Found {command.Id}"))
            );
        }
        
        // product.Name = command.Name;
        // product.Price = command.Price;
        // product.Stock = command.Stock;
        // product.Category = command.Category;
        // product.ImagesUrls = command.ImagesUrls;
        //
        // await repository.SaveChangesAsync(cancellationToken);
        //
        // return await Task.FromResult(
        //     Result.Success(new UpdateProductResponseDto(
        //         product.Id,
        //         product.Name,
        //         product.Price,
        //         product.Stock,
        //         product.Category,
        //         product.ImagesUrls
        //     ))
        // );
        return await Task.FromResult(
            Result.Failure<UpdateProductResponseDto>(Error.NotFound("400", $"Product Not Found {command.Id}"))
        );
    }
}