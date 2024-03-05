using EventPad.Common;
using EventPad.Common.Exceptions;
using EventPad.Common.Extensions;
using EventPad.Common.Responses;
using FluentValidation;

namespace EventPad.Api;

public class ExceptionsMiddleware
{
    private readonly RequestDelegate next;

    public ExceptionsMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        ErrorResponse response = null;
        var statusCode = StatusCodes.Status200OK;
        try
        {
            await next.Invoke(context);
        }
        catch (ForbidAccessException fe)
        {
            response = fe.ToErrorResponse();
            statusCode = StatusCodes.Status403Forbidden;
        }
        catch (ProcessException pe)
        {
            response = pe.ToErrorResponse();
            statusCode = StatusCodes.Status400BadRequest;
        }
        catch (ValidationException ve)
        {
            response = ve.ToErrorResponse();
            statusCode = StatusCodes.Status400BadRequest;
        }
        catch (Exception pe)
        {
            response = pe.ToErrorResponse();
            statusCode = StatusCodes.Status400BadRequest;
        }
        finally
        {
            if (response is not null)
            {
                context.Response.StatusCode = statusCode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(response.ToJsonString());
                await context.Response.StartAsync();
                await context.Response.CompleteAsync();
            }
        }
    }
}
