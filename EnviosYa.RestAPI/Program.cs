using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using EnviosYa.Application;
using EnviosYa.Application.Features.Auth.Login.Commands;
using EnviosYa.Application.Features.Auth.Login.DTOs;
using EnviosYa.Application.Features.Auth.Register.Commands.Create;
using EnviosYa.Application.Features.Auth.Register.DTOs;
using EnviosYa.Application.Features.Product.Commands.Create;
using EnviosYa.Application.Features.Product.Commands.Update;
using EnviosYa.Application.Features.Product.DTOs;
using EnviosYa.Application.Features.Product.Queries.GetFilterCategory;
using EnviosYa.Domain.Constants;
using EnviosYa.Infrastructure;
using EnviosYa.Infrastructure.Authentication;
using EnviosYa.RestAPI.Endpoints;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings!.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = 
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminRole", policy => policy.RequireClaim(ClaimTypes.Role, nameof(RolUser.Admin)));
    options.AddPolicy("ClienteRole", policy => policy.RequireClaim(ClaimTypes.Role, nameof(RolUser.Cliente)));
});

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
        document.Components.SecuritySchemes["Bearer"] = new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            Description = "Enter your JWT token"
        };
        
        return Task.CompletedTask;
    });
});
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<IValidator<LoginUserDto>, LoginUserCommandValidator>();
builder.Services.AddScoped<IValidator<CreateProductDto>, CreateProductCommandValidator>();
builder.Services.AddScoped<IValidator<UpdateProductDto>, UpdateProductCommandValidator>();
builder.Services.AddScoped<IValidator<CreateUserDto>, CreateUserCommandValidator>();
builder.Services.AddScoped<IValidator<GetCategoryProductDto>, GetFilterCategoryProductQueryValidator>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false)
    .AddEnvironmentVariables();

builder.WebHost.UseUrls("http://+:8081");

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowFrontend");
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

app.MapAuthEndpoints();
app.MapUserEndpoints();
app.MapProductsEndpoints();
app.MapCartEndpoints();
app.MapCartItemsEndpoints();

app.Run();
