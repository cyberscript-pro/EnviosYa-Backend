using EnviosYa.Application.Common.Services;
using EnviosYa.Application.Features.Product.Commands.Update;
using EnviosYa.Application.Features.Product.Queries.GetFilterCategory;

namespace EnviosYa.Application.Features.Product.DTOs;

public record GetCategoryProductDto(string Category);

public static class GetCategoryProductToQuery
{
    public static GetFilterCategoryProductQuery ToCommand(this GetCategoryProductDto dto)
    {
        // CategoryMapper.TryParseCategory(dto.Category, out var category);
        //
        return new GetFilterCategoryProductQuery
        {
            //Category = category,
        };
    }
}