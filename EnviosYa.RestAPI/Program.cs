using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Application.Common.Dispatching;
using EnviosYa.Infrastructure;
using EnviosYa.RestAPI.Endpoints;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<ICommandDispatcher, CommandDispatcher>();
builder.Services.Scan(scan => scan
    .FromAssembliesOf(typeof(Program))
    // .AddClasses(
    //     classes =>
    //         classes.AssignableTo(typeof(IQueryHandler<,>)),
    //     publicOnly: false)
    // .AsImplementedInterfaces()
    // .WithScopedLifetime()
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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.MapProductsEndpoints();
app.UseHttpsRedirection();

app.Run();
