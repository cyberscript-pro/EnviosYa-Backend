using EnviosYa.Application.Common;
using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace EnviosYa.Application.Features.Cart.Queries.GetOne;

public class GetOneCartQueryHandler(IRepository repository): IQueryHandler<GetOneCartQuery, GetOneCartResponseDto>
{
    public async Task<Result<GetOneCartResponseDto>> Handle(GetOneCartQuery query, CancellationToken cancellationToken = default)
    {
        var cart = await repository.Carts
            .Where(c => c.UserId == query.Id && c.IsAvailable)
            .Select(c => new GetOneCartResponseDto(
                c.Id,
                c.UserId,
                c.Items.Select(i => new CartItemDto(
                    i.Id,
                    i.ProductId,
                    i.Cantidad,
                    i.Producto == null ? null : new ProductDto(
                        i.Producto.Id,
                        i.Producto.Name,
                        i.Producto.Price,
                        i.Producto.Category
                    )
                )).ToList()
            ))
            .FirstOrDefaultAsync(cancellationToken);

        if (cart is null)
        {
            return await Task.FromResult(Result.Failure<GetOneCartResponseDto>(Error.NotFound("400", "Cart not found")));
        }

        return await Task.FromResult(Result.Success(cart));
    }
}