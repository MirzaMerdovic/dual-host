using Microsoft.AspNetCore.Builder;

namespace ConsoleOut.DualHost.AspNet
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<HealthCheckMiddleware>();
        }
    }
}