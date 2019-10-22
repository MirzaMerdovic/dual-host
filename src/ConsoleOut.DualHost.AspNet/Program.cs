using ConsoleOut.DualHost.AspNet.Hosts;
using ConsoleOut.DualHost.AspNet.Services;
using ConsoleOut.DualHost.Hosts.AspNet;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleOut.DualHost.AspNet
{
    internal class Program
    {
        internal static async Task Main(string[] args)
        {
            var source = new CancellationTokenSource();

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

            await host.StartAsync(source.Token);

            var webHost = HealthCheckServerHost.BuildHost<MyHealthService>();
            var kestrelRunner = webHost.RunAsync(source.Token);

            await host.WaitForShutdownAsync(source.Token);
        }
    }
}