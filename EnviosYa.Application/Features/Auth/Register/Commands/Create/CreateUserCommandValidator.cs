using EnviosYa.Application.Features.Auth.Register.DTOs;
using FluentValidation;

namespace EnviosYa.Application.Features.Auth.Register.Commands.Create;

public class CreateUserCommandValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserCommandValidator()
    {
        RuleFor(u => u.FullName)
            .NotEmpty().WithMessage("Full name cannot be empty")
            .MaximumLength(100).WithMessage("Full name max length 100");
        
        RuleFor(u => u.Nickname)
            .NotEmpty().WithMessage("Nickname is required.")
            .MaximumLength(50).WithMessage("Nickname must not exceed 50 characters.")
            .MinimumLength(2).WithMessage("Nickname must not exceed 2 characters.");
        
        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("Email is required.")
            .MaximumLength(100).WithMessage("Email max length 100");

        RuleFor(u => u.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must not exceed 8 characters.");
        
        RuleFor(u => u.Role)
            .IsInEnum().WithMessage("Role is invalid.");

        RuleFor(u => u.ProfilePicture);

        RuleFor(u => u.Phone);
    }
}