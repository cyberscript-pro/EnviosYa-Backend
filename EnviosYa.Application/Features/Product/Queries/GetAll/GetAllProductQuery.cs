using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Application.Features.Product.Commands.Create;

namespace EnviosYa.Application.Features.Product.Queries.GetAll;

public class GetAllProductQuery : IQuery<List<GetAllProductResponseDto>>
{
    public required string Language { get; init; }
}