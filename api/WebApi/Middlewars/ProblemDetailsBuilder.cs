using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Middlewars;

public class ProblemDetailsBuilder
{
    public async Task Build(HttpContext context)
    {
        var exception = context.Features.Get<IExceptionHandlerPathFeature>()?.Error;

        ProblemDetails problem;

        switch (exception)
        {
            case FluentValidation.ValidationException validationEx:
                problem = new ValidationProblemDetails(
                    validationEx.Errors
                        .GroupBy(e => e.PropertyName)
                        .ToDictionary(
                            g => g.Key,
                            g => g.Select(e => e.ErrorMessage).ToArray()
                        ))
                {
                    Title = "Validation failed",
                    Detail = exception.Message,
                    Status = StatusCodes.Status400BadRequest,
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
                };

                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                break;


            case DbUpdateException dbEx:
                problem = new ProblemDetails
                {
                    Title = "Database update error",
                    Detail = dbEx.InnerException?.Message ?? dbEx.Message,
                    Status = StatusCodes.Status409Conflict,
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.8"
                };

                context.Response.StatusCode = StatusCodes.Status409Conflict;

                break;

            default:
                problem = new ProblemDetails
                {
                    Title = "An unexpected error occurred",
                    Detail = exception?.Message,
                    Status = StatusCodes.Status500InternalServerError,
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
                };

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                break;
        }

        context.Response.ContentType = "application/problem+json";

        await context.Response.WriteAsJsonAsync(problem);
    }
}
