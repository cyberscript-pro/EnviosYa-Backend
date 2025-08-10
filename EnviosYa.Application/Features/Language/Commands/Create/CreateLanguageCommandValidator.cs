using EnviosYa.Application.Features.Language.DTOs;
using EnviosYa.Domain.Common;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EnviosYa.Application.Features.Language.Commands.Create;

public class CreateLanguageCommandValidator : AbstractValidator<CreateLanguageDto>
{
    public CreateLanguageCommandValidator(IRepository repository)
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Code is required.")
            .MustAsync(async(code, cancellationToken) =>
            {
                var language = await repository.Languages.FirstOrDefaultAsync(l => l.Code == code, cancellationToken);

                return language is null;
            })
            .WithMessage("Code already exists.");
        
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.");
    }
}