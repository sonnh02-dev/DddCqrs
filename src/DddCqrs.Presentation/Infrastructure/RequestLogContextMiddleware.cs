using Serilog.Context;

namespace DddCqrs.Presentation.Infrastructure;

#pragma warning disable CA1515 // Consider making public types internal
public class RequestLogContextMiddleware
#pragma warning restore CA1515 // Consider making public types internal
{
    private readonly RequestDelegate _next;

    public RequestLogContextMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public Task InvokeAsync(HttpContext context)
    {
        using (LogContext.PushProperty("CorrelationId", context.TraceIdentifier))
        {
            return _next(context);
        }
    }
}
