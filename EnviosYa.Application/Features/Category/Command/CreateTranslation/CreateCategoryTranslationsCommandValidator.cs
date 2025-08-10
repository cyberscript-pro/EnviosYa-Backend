using EnviosYa.Application.Features.Category.DTOs;
using EnviosYa.Domain.Common;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EnviosYa.Application.Features.Category.Command.CreateTranslation;

public class CreateCategoryTranslationsCommandValidator : AbstractValidator<CreateCategoryTranslationDto>
{
    public CreateCategoryTranslationsCommandValidator(IRepository repository)
    {
        RuleFor(x => x.LanguageId)
            .NotEmpty().WithMessage("LanguageId is required.")
            .MustAsync(async(id, cancellationToken) =>
            {
                if (!Guid.TryParse(id, out var languageId)) return false;
                
                var language = await repository.Languages.FirstOrDefaultAsync(l => l.Id == languageId, cancellationToken);
                    
                return language is not null;
            })
            .WithMessage("LanguageId is not valid.");
        
        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("CategoryId is required.")
            .MustAsync(async(id, cancellationToken) =>
            {
                if (!Guid.TryParse(id, out var categoryId)) return false;
                
                var category = await repository.Categories.FirstOrDefaultAsync(ca => ca.Id == categoryId, cancellationToken);
                    
                return category is not null;
            })
            .WithMessage("CategoryId is not valid.");
        
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.");
        
    }
}