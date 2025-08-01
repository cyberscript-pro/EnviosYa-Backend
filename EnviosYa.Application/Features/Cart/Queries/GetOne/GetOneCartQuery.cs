using EnviosYa.Application.Common.Abstractions;

namespace EnviosYa.Application.Features.Cart.Queries.GetOne;

public class GetOneCartQuery: IQuery<GetOneCartResponseDto>
{
    public required Guid Id { get; set; }
}