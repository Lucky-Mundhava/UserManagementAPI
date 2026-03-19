using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace UserManagementAPI.Middleware
{
    public class SimpleAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<SimpleAuthenticationMiddleware> _logger;

        public SimpleAuthenticationMiddleware(RequestDelegate next, ILogger<SimpleAuthenticationMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Skip authentication for Swagger UI endpoints 
            // so we can still load the UI without tokens
            var path = context.Request.Path.Value;
            if (path.StartsWith("/swagger") || path.StartsWith("/favicon"))
            {
                await _next(context);
                return;
            }

            if (!context.Request.Headers.TryGetValue("Authorization", out var authHeader) || 
                authHeader != "Bearer valid-secret-token")
            {
                _logger.LogWarning("Unauthorized access attempt.");
                
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync("{\"error\": \"Unauthorized. Invalid or missing token.\"}");
                
                return; // Short-circuit pipeline
            }

            await _next(context); // Token valid, proceed to next middleware
        }
    }
}
