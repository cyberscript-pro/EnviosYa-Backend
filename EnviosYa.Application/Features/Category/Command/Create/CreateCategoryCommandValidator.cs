using EnviosYa.Application.Features.Category.DTOs;
using EnviosYa.Domain.Common;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EnviosYa.Application.Features.Category.Command.Create;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryDto>
{
    public CreateCategoryCommandValidator(IRepository repository)
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
        
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.");
    }
}