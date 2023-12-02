using Driver_WebAPI.Models;
using Newtonsoft.Json;
using System.Net;

namespace Driver_WebAPI.Middlewares;

/// <summary>
/// Extension class for global exception handler
/// </summary>
public class ExceptionMiddlewareHandler
{
    private readonly RequestDelegate _next;

    public ExceptionMiddlewareHandler(RequestDelegate next)
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

    /// <summary>
    /// exception handler method as extension method fires once exception founded.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="exception"></param>
    /// <returns></returns>
    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var response = context.Response;
        ResponseModel exResult = new ResponseModel();

        switch (exception)
        {
            case ApplicationException ex:
                exResult.responseCode = (int)HttpStatusCode.BadRequest;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                exResult.responseMessage = exception.Message;
                break;

            default:
                exResult.responseCode = (int)HttpStatusCode.InternalServerError;
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                exResult.responseMessage = exception.Message;
                break;
        }
        var exResponse = JsonConvert.SerializeObject(exResult);
        await context.Response.WriteAsync(exResponse);
    }
}