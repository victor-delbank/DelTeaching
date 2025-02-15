using System.Text.Json;
using DelTeaching.Domain.Exceptions;

namespace API.Middlewares;
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }
    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = context.Response;
        response.ContentType = "application/json";

        int statusCode = StatusCodes.Status500InternalServerError;
        string message = "Ocorreu um erro no sistema!";
        string code = "INTERNAL_SERVER_ERROR";

        if (exception is DelTeachingException delTeachingException)
        {
            statusCode = StatusCodes.Status400BadRequest;
            message = delTeachingException.Message;
            code = delTeachingException.Code ?? "BAD_REQUEST";
        }

        response.StatusCode = statusCode;

        var errorResponse = new
        {
            message = message,
            code = code,
        };

        var result = JsonSerializer.Serialize(errorResponse);
        await response.WriteAsync(result);
    }
}