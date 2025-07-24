using EnviosYa.Application.Common;
using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace EnviosYa.Application.Features.Product.Commands.Delete;

public class DeleteProductCommandHandler(IRepository repository) : ICommandHandler<DeleteProductCommand, DeleteProductResponseDto>
{
    public async Task<Result<DeleteProductResponseDto>> Handle(DeleteProductCommand command, CancellationToken cancellationToken = default)
    {
        var product = await repository.Products.FirstOrDefaultAsync(p => p.Id == command.Id && p.IsAvailable, cancellationToken);

        if (product is null)
        {
            return await Task.FromResult(
                Result.Failure<DeleteProductResponseDto>(Error.NotFound("400", "Product not found"))
            );
        }

        product.IsAvailable = false;
        await repository.SaveChangesAsync(cancellationToken);

        return await Task.FromResult(Result.Success(new DeleteProductResponseDto()));
    }
}