using System.Text.Json.Serialization;
using EnviosYa.Application;
using EnviosYa.Application.Features.Product.Commands.Create;
using EnviosYa.Application.Features.Product.Commands.Update;
using EnviosYa.Application.Features.Product.DTOs;
using EnviosYa.Application.Features.Product.Queries.GetFilterCategory;
using EnviosYa.Infrastructure;
using EnviosYa.RestAPI.Endpoints;
using FluentValidation;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Info.Title = "EnviosYa API";
        document.Info.Version = "v1";
        document.Info.Description = "A e-commerce platform API for Cuban persons";
        document.Info.Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Ziphir Team",
            Email = "contact@ziphir.com"
        };
        
        document.Components ??= new Microsoft.OpenApi.Models.OpenApiComponents();
        // document.Components.SecuritySchemes["Bearer"] = new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        // {
        //     Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        //     Scheme = "bearer",
        //     BearerFormat = "JWT",
        //     Description = "Enter your JWT token"
        // };
        
        return Task.CompletedTask;
    });
});
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<IValidator<CreateProductDto>, CreateProductCommandValidator>();
builder.Services.AddScoped<IValidator<UpdateProductDto>, UpdateProductCommandValidator>();
builder.Services.AddScoped<IValidator<GetCategoryProductDto>, GetFilterCategoryProductQueryValidator>();

var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference(options =>
{
    options
        .WithTitle("EnviosYa API Documentation")
        .WithTheme(ScalarTheme.Purple)
        .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient)
        .WithModels(true)
        .WithSearchHotKey("k");
});

app.UseHttpsRedirection();

app.MapProductsEndpoints();

app.Run();
