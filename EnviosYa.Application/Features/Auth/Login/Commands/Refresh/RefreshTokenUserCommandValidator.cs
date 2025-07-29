using EnviosYa.Application.Features.Auth.Login.DTOs;
using FluentValidation;

namespace EnviosYa.Application.Features.Auth.Login.Commands.Refresh;

public class RefreshTokenUserCommandValidator : AbstractValidator<RefreshTokenDto>
{
    public RefreshTokenUserCommandValidator()
    {
        RuleFor(x => x.RefreshToken).NotNull().NotEmpty().WithMessage("RefreshToken required");
    }
}