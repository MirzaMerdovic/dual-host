using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConsoleOut.DualHost.Hosts
{
    public static class HostExtensions
    {
        public static IServiceCollection AddNancyHealthCheckServer<THostedService>(this IServiceCollection services, HostBuilderContext context)
            where THostedService : class, IHostedService
        {
            return
                services
                    .Configure<NancySeverOptions>(context.Configuration.GetSection("NancySeverOptions"))
                    .AddSingleton<INancyHostFactory, NancyHostFactory>()
                    .AddSingleton<THostedService>()
                    .AddSingleton<IHostedService>(x => new HealthCheckHostedServiceDecorator<THostedService>(
                        x.GetRequiredService<THostedService>(),
                        x.GetRequiredService<INancyHostFactory>()));
        }
    }
}