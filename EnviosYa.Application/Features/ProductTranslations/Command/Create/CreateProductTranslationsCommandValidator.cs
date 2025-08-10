using EnviosYa.Application.Features.ProductTranslations.DTOs;
using EnviosYa.Domain.Common;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EnviosYa.Application.Features.ProductTranslations.Command.Create;

public class CreateProductTranslationsCommandValidator : AbstractValidator<CreateProductTranslationDto>
{
    public CreateProductTranslationsCommandValidator(IRepository repository)
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
        
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("ProductId is required.")
            .MustAsync(async(id, cancellationToken) =>
            {
                if (!Guid.TryParse(id, out var productId)) return false;
                
                var product = await repository.Products.FirstOrDefaultAsync(p => p.Id == productId, cancellationToken);
                    
                return product is not null;
            })
            .WithMessage("ProductId is not valid.");
        
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.");
        
    }
}