using EnviosYa.Application.Features.Auth.Register.DTOs;
using EnviosYa.Domain.Common;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EnviosYa.Application.Features.Auth.Register.Commands.Create;

public class CreateUserCommandValidator : AbstractValidator<CreateUserDto>
{
    public readonly IRepository Repository;
    public CreateUserCommandValidator(IRepository repository)
    {
        Repository = repository;
        RuleFor(u => u.FullName)
            .NotEmpty().WithMessage("Full name cannot be empty")
            .MaximumLength(100).WithMessage("Full name max length 100");
        
        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("Email is required.")
            .MaximumLength(100).WithMessage("Email max length 100")
            .MustAsync(async (email, cancellationToken) =>
            {
                var userExists = await Repository.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
                return userExists is null;
            }).WithMessage("Email already exists");

        RuleFor(u => u.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must not exceed 8 characters.");
        
        RuleFor(u => u.Role)
            .IsInEnum().WithMessage("Role is invalid.");

        RuleFor(u => u.ProfilePicture);

        RuleFor(u => u.Phone);
    }
}