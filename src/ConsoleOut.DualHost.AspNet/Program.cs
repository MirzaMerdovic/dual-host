using ConsoleOut.DualHost.AspNet.Hosts;
using ConsoleOut.DualHost.AspNet.Services;
using ConsoleOut.DualHost.Hosts.AspNet;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Threading.Tasks;

namespace ConsoleOut.DualHost.AspNet
{
    internal class Program
    {
        internal static Task Main(string[] args)
        {
            IHost host = new HostBuilder()
                .ConfigureHostConfiguration(configHost =>
                {
                    configHost.SetBasePath(Directory.GetCurrentDirectory());
                    configHost.AddJsonFile("hostsettings.json");
                })
                .ConfigureServices((hostContext, configSvc) =>
                {
                    configSvc.AddLogging();
                    configSvc.AddHostedService<ConsoleHostedService>();
                })
                .UseConsoleLifetime()
                .Build();

            var consoleRunner = host.RunAsync();

            var webHost = HealthCheckServerHost.BuildHost<MyHealthService>();
            var kestrelRunner = webHost.RunAsync();

            return Task.WhenAll(consoleRunner, kestrelRunner);
        }
    }
}