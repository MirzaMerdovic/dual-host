using Microsoft.AspNetCore.Builder;

namespace ConsoleOut.DualHost.AspNet30
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<HealthCheckMiddleware>();
        }
    }
}