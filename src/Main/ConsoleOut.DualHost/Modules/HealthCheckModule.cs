using Nancy;

namespace ConsoleOut.DualHost.Hosts
{
    public class HealthCheckModule : NancyModule
    {
        public HealthCheckModule()
        {
            Get("/health", x => {
                return "OK";
            });
        }
    }
}