using Microsoft.AspNetCore.Mvc.Filters;

namespace DiagnosticApi.Filters;

public class RequestLoggingFilter : IActionFilter
{
    private readonly ILogger<RequestLoggingFilter> _logger;

    public RequestLoggingFilter(ILogger<RequestLoggingFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var action = context.ActionDescriptor.DisplayName;
        _logger.LogInformation($"[Before] Executing action: {action}");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        var action = context.ActionDescriptor.DisplayName;
        _logger.LogInformation($"[After] Executed action: {action}");
    }
}