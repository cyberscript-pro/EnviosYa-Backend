using EnviosYa.Application.Features.Auth.Login.DTOs;
using FluentValidation;

namespace EnviosYa.Application.Features.Auth.Login.Commands.GoogleAuth;

public class GoogleAuthCommandValidator : AbstractValidator<GoogleAuthDto>
{
    public GoogleAuthCommandValidator()
    {
        RuleFor(x => x.IdToken).NotNull().NotEmpty().WithMessage("IdToken required");
    }
}