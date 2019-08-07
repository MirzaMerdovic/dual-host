using ConsoleOut.DualHost.AspNet.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Net;

namespace ConsoleOut.DualHost.AspNet.Hosts
{
    public static class HealthCheckServerHost
    {
        public static IWebHost BuildHost<THealthService>(Action<WebHostBuilderContext, IServiceCollection> configure = null)
            where THealthService : class, IHealthService
        {
            configure = configure ?? ((_, __) => { });

            IWebHost host = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseConfiguration(
                    new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("hostsettings.json")
                        .Build())
                .UseKestrel((ctx, ops) =>
                {
                    var ip = ctx.Configuration.GetSection("KestrelOptions").GetValue<string>("Ip");
                    var port = ctx.Configuration.GetSection("KestrelOptions").GetValue<int>("Port");

                    ops.Listen(IPAddress.Parse(ip), port);
                })
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