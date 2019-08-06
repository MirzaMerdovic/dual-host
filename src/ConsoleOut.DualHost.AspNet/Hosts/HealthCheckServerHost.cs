using ConsoleOut.DualHost.AspNet.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace ConsoleOut.DualHost.AspNet.Hosts
{
    public static class HealthCheckServerHost
    {
        public static IWebHost BuildHost<THealthService>(Action<WebHostBuilderContext, IServiceCollection> configure = null)
            where THealthService : class, IHealthService
        {
            configure ??= ((_, __) => { });

            IWebHost host = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseKestrel()
                .ConfigureServices((ctx, services) =>
                {
                    services.AddLogging();
                    services.AddSingleton<IHealthService, THealthService>();
                })
                .UseStartup<Startup>()
                .ConfigureServices(configure)
                .Build();

            return host;
        }
    }
}