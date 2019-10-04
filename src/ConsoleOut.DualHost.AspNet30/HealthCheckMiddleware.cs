using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ConsoleOut.DualHost.AspNet30
{
    internal class HealthCheckMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Services.IHealthService _health;
        private readonly ILogger _logger;

        public HealthCheckMiddleware(RequestDelegate next, Services.IHealthService health, ILogger<HealthCheckMiddleware> logger)
        {
            _next = next;
            _health = health;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Path.Equals(PathString.FromUriComponent("/health"), StringComparison.InvariantCultureIgnoreCase))
            {
                await _next(context);

                return;
            }

            var isHealth = await _health.IsHealthy();

            context.Response.StatusCode = isHealth ? StatusCodes.Status200OK : StatusCodes.Status503ServiceUnavailable;
            await context.Response.WriteAsync("Endpoint is healthy");
        }
    }
}