using EnviosYa.Application.Features.Auth.Login.DTOs;
using FluentValidation;

namespace EnviosYa.Application.Features.Auth.Login.Commands;

public class LoginUserCommandValidator : AbstractValidator<LoginUserDto>
{
    public LoginUserCommandValidator()
    {
        RuleFor(user => user.Email)
            .EmailAddress()
            .WithMessage("Invalid email address");
        
        RuleFor(user => user.Password)
            .NotEmpty().WithMessage("Invalid password")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long");
    }
}