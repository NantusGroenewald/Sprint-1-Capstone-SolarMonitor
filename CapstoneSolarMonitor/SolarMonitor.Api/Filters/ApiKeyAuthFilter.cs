using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SolarMonitor.Api.Filters;

public class ApiKeyAuthFilter : IAsyncActionFilter
{
    private readonly IConfiguration _configuration;
    private const string ApiKeyHeaderName = "X-API-KEY";

    public ApiKeyAuthFilter(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey))
        {
            context.Result = new UnauthorizedObjectResult(new { Message = "API Key is missing." });
            return;
        }

        var expectedApiKey = _configuration.GetValue<string>("ApiSettings:ApiKey");

        if (!expectedApiKey!.Equals(extractedApiKey))
        {
            context.Result = new UnauthorizedObjectResult(new { Message = "Invalid API Key." });
            return;
        }

        await next();
    }
}