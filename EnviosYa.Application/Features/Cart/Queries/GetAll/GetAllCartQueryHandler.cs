using EnviosYa.Application.Common;
using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace EnviosYa.Application.Features.Cart.Queries.GetAll;

public class GetAllCartQueryHandler(IRepository repository) : IQueryHandler<GetAllCartQuery, List<GetAllCartResponseDto>>
{
    public async Task<Result<List<GetAllCartResponseDto>>> Handle(GetAllCartQuery query, CancellationToken cancellationToken = default)
    {
        var carts = await repository.Carts.Where(c => c.IsAvailable).ToListAsync(cancellationToken);

        var response = carts.Select(GetAllCartToResponse.MapToResponse).ToList();

        return await Task.FromResult(Result.Success(response));
    }
}