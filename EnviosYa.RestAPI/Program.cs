using EnviosYa.Application;
using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Application.Features.Product.Commands.Create;
using EnviosYa.Infrastructure;
using EnviosYa.RestAPI.Data.Products;
using EnviosYa.RestAPI.Endpoints;
using EnviosYa.RestAPI.Middleware;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<IValidator<CreateProductDto>, CreateProductCommandValidator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.MapProductsEndpoints();
app.UseHttpsRedirection();

app.Run();
