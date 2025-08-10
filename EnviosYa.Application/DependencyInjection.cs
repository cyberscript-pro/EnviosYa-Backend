using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Application.Features.Auth.Login.Commands.Login;
using EnviosYa.Application.Features.Auth.Login.Commands.Refresh;
using EnviosYa.Application.Features.Auth.Login.DTOs;
using EnviosYa.Application.Features.Auth.Register.Commands.Create;
using EnviosYa.Application.Features.Auth.Register.DTOs;
using EnviosYa.Application.Features.CartItem.Commands.Create;
using EnviosYa.Application.Features.CartItem.Commands.Delete;
using EnviosYa.Application.Features.CartItem.DTOs;
using EnviosYa.Application.Features.Category.Command.Create;
using EnviosYa.Application.Features.Category.Command.CreateTranslation;
using EnviosYa.Application.Features.Category.DTOs;
using EnviosYa.Application.Features.Language;
using EnviosYa.Application.Features.Language.Commands.Create;
using EnviosYa.Application.Features.Language.DTOs;
using EnviosYa.Application.Features.Product.Commands.Create;
using EnviosYa.Application.Features.Product.Commands.Update;
using EnviosYa.Application.Features.Product.DTOs;
using EnviosYa.Application.Features.Product.Queries.GetFilterCategory;
using EnviosYa.Application.Features.ProductTranslations.Command.Create;
using EnviosYa.Application.Features.ProductTranslations.DTOs;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace EnviosYa.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssembliesOf(typeof(ICommandHandler<,>))
            .AddClasses(
                classes =>
                    classes.AssignableTo(typeof(IQueryHandler<,>)),
                publicOnly: false)
            .AsImplementedInterfaces()
            .WithScopedLifetime()
            .AddClasses(classes =>
                    classes.AssignableTo(typeof(ICommandHandler<,>)),
                publicOnly: false)
            .AsImplementedInterfaces()
            .WithScopedLifetime()
            .AddClasses(classes =>
                    classes.AssignableTo(typeof(ICommandHandler<>)),
                publicOnly: false)
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        
        
        services.AddScoped<IValidator<LoginUserDto>, LoginUserCommandValidator>();
        services.AddScoped<IValidator<RefreshTokenDto>, RefreshTokenUserCommandValidator>();
        services.AddScoped<IValidator<CreateUserDto>, CreateUserCommandValidator>();
        services.AddScoped<IValidator<CreateProductDto>, CreateProductCommandValidator>();
        services.AddScoped<IValidator<UpdateProductDto>, UpdateProductCommandValidator>();
        services.AddScoped<IValidator<CreateCartItemDto>, CreateCartItemCommandValidator>();
        services.AddScoped<IValidator<DeleteCartItemDto>, DeleteCartItemCommandValidator>();
        services.AddScoped<IValidator<GetCategoryProductDto>, GetFilterCategoryProductQueryValidator>();
        services.AddScoped<IValidator<CreateLanguageDto>, CreateLanguageCommandValidator>();
        services.AddScoped<IValidator<CreateCategoryDto>, CreateCategoryCommandValidator>();
        services.AddScoped<IValidator<CreateCategoryTranslationDto>, CreateCategoryTranslationsCommandValidator>();
        services.AddScoped<IValidator<CreateProductTranslationDto>, CreateProductTranslationsCommandValidator>();

        return services;
    }
}