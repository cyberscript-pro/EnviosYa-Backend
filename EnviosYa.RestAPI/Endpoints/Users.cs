using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Application.Features.Auth.Register.Commands.Create;
using EnviosYa.Application.Features.Auth.Register.DTOs;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace EnviosYa.RestAPI.Endpoints;

public static class Users
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        var usersGroup = app.MapGroup("/users")
            .WithTags("Users")
            .WithOpenApi();

        usersGroup.MapPost("/register", async ([FromBody] CreateUserDto dto, [FromServices] IValidator<CreateUserDto> validator, [FromServices] ICommandHandler<CreateUserCommand, CreateUserResponseDto> handler) =>
        {
            var validationResult = await validator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
                return Results.BadRequest(errors);
            }

            var command = dto.MapToCommand();
            var result = await handler.Handle(command);
            
            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
        })
        .WithOpenApi(operation => new(operation)
        {
            Summary = "Creates a user",
            Description = "Creates a user in the system",
            Tags = new List<Microsoft.OpenApi.Models.OpenApiTag> { new() { Name = "Users" } }
        });

        usersGroup.MapPost("/upload-profile-picture", async (HttpRequest request, IWebHostEnvironment env) =>
            {
                if (!request.HasFormContentType)
                    Results.BadRequest("Debe ser multipart/form-data");

                var form = await request.ReadFormAsync();
                var file = form.Files["file"];

                if (file == null || file.Length == 0)
                    return Results.BadRequest("No se envió ningún archivo");

                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

                if (!allowedExtensions.Contains(extension))
                    return Results.BadRequest("Formato no permitido");

                var fileName = $"{Guid.NewGuid()}{extension}";

                using var stream = file.OpenReadStream();
                using var client = new HttpClient();

                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Im95bG9qb3B3emhjZnBwaHpmaGtjIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NTM4OTk2MzEsImV4cCI6MjA2OTQ3NTYzMX0.SPA5SLyXKrM_9xbAiw0t0ShLO80Uk6NuNxUP0esO3kI");

                var content = new StreamContent(stream);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
                
                var uploadUrl = $"https://oylojopwzhcfpphzfhkc.supabase.co/storage/v1/object/profile-pictures/{fileName}";
                
                var response = await client.PostAsync(uploadUrl, content);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    return Results.Problem("Error subiendo imagen: " + error);
                }
                
                var publicUrl = $"https://oylojopwzhcfpphzfhkc.supabase.co/storage/v1/object/public/profile-pictures/{fileName}";

                return Results.Ok(new { publicUrl });
            })
            .Accepts<IFormFile>("multipart/form-data")
            .Produces(200)
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Upload profile picture",
                Description = "Upload profile picture in the system",
                Tags = new List<Microsoft.OpenApi.Models.OpenApiTag> { new() { Name = "Users" } }
            });
    }
}