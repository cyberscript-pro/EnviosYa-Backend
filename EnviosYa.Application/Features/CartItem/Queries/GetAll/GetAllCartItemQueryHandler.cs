using EnviosYa.Application.Common;
using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace EnviosYa.Application.Features.CartItem.Queries.GetAll;

public class GetAllCartItemQueryHandler(IRepository repository) : IQueryHandler<GetAllCartItemQuery, List<GetAllCartItemResponseDto>>
{
    public async Task<Result<List<GetAllCartItemResponseDto>>> Handle(GetAllCartItemQuery query, CancellationToken cancellationToken = default)
    {
        var items = await repository.CartItems.Where(i => i.IsAvailable).OrderBy(i => i.CartId).ToListAsync(cancellationToken);
        
        var response = items.Select(GetAllCartItemToResponse.MapToResponse).ToList();

        return await Task.FromResult(
            Result.Success(response)
        );
    }
}