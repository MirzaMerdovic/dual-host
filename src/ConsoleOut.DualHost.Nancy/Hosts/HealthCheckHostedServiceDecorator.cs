using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleOut.DualHost.Hosts
{
    public class HealthCheckHostedServiceDecorator<THostedService> : IHostedService
        where THostedService : class, IHostedService
    {
        private readonly THostedService _hostedService;
        private readonly INancyHostFactory _hostFactory;

        public HealthCheckHostedServiceDecorator(THostedService hostedService, INancyHostFactory hostFactory)
        {
            _hostedService = hostedService;
            _hostFactory = hostFactory ?? throw new ArgumentNullException(nameof(hostFactory));
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _hostFactory.Get().Start();

            return _hostedService.StartAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return _hostedService.StopAsync(cancellationToken);
        }
    }
}
