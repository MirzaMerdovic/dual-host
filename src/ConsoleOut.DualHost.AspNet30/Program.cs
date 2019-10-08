using ConsoleOut.DualHost.AspNet30.Hosts;
using ConsoleOut.DualHost.AspNet30.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace ConsoleOut.DualHost.AspNet30
{
    internal class Program
    {
        public static Task Main(string[] args)
        {

            IHost builder =
                Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(builder =>
                {
                    builder.UseSetting(WebHostDefaults.DetailedErrorsKey, "true");
                    builder.UseStartup<Startup>();

                    builder.ConfigureServices(x => x.AddTransient<IHealthService, MyHealthService>());
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<ConsoleHostedService>();
                })
                .Build();

            return builder.RunAsync();
        }
    }
}