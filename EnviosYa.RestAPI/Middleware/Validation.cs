using FluentValidation;

namespace EnviosYa.RestAPI.Middleware;

public class ValidationFilter<T> where T : class
{
    private readonly IValidator<T> _validator;

    public ValidationFilter(IValidator<T> validator)
    {
        _validator = validator;
    }

    public async Task<T> ValidateAsync(HttpContext context)
    {
        var dto = await context.Request.ReadFromJsonAsync<T>();
        var validationResult = await _validator.ValidateAsync(dto!);

        if (!validationResult.IsValid)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));
            throw new ValidationException(validationResult.Errors);
        }

        return dto!;
    }
}
