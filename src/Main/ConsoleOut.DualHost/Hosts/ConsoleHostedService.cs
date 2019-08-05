using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleOut.DualHost.Hosts
{
    public class ConsoleHostedService : IHostedService
    {
        private readonly IApplicationLifetime _lifetime;
        private readonly ILogger _logger;

        public ConsoleHostedService(IApplicationLifetime lifetime, ILogger<ConsoleHostedService> logger)
        {
            _lifetime = lifetime ?? throw new ArgumentNullException(nameof(lifetime));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _lifetime.ApplicationStarted.Register(Started);
            _lifetime.ApplicationStopping.Register(Stopping);
            _lifetime.ApplicationStopped.Register(Stopped);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopped.");

            return Task.CompletedTask;
        }

        private void Started()
        {
        }

        private void Stopping()
        {
        }

        private void Stopped()
        {
        }
    }
}
