using ConsoleOut.DualHost.Hosts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Threading.Tasks;

namespace ConsoleOut.DualHost
{
    internal class Program
    {
        internal static Task Main(string[] args)
        {
            IHost host = new HostBuilder()
                .ConfigureHostConfiguration(configHost =>
                {
                    configHost.SetBasePath(Directory.GetCurrentDirectory());
                    configHost.AddJsonFile("hostsettings.json", optional: true);
                })
                .ConfigureServices((hostContext, configSvc) =>
                {
                    configSvc.AddLogging();
                    configSvc.AddNancyHealthCheckServer<ConsoleHostedService>(hostContext);
                })
                .UseConsoleLifetime()
                .Build();

            return host.RunAsync();
        }
    }
}