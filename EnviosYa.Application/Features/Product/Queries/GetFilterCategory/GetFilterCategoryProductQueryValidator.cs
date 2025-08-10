using EnviosYa.Application.Common.Services;
using EnviosYa.Application.Features.Product.DTOs;
using FluentValidation;

namespace EnviosYa.Application.Features.Product.Queries.GetFilterCategory;

public class GetFilterCategoryProductQueryValidator : AbstractValidator<GetCategoryProductDto>
{
    public GetFilterCategoryProductQueryValidator()
    {
        // RuleFor(p => p.Category)
        //     .NotEmpty().WithMessage("Category is required.")
        //     .Must(cat => CategoryMapper.TryParseCategory(cat, out var category)).WithMessage("Category must be a valid category.");
    }
}