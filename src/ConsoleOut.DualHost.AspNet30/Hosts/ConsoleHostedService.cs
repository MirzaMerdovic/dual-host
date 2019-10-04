using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleOut.DualHost.AspNet30.Hosts
{
    internal class ConsoleHostedService : IHostedService
    {
        private readonly IHostApplicationLifetime _lifetime;
        private readonly ILogger _logger;

        public ConsoleHostedService(IHostApplicationLifetime lifetime, ILogger<ConsoleHostedService> logger)
        {
            _lifetime = lifetime;
            _logger = logger;
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
            _logger.LogInformation("Console host is running.");
        }

        private void Stopping()
        {
        }

        private void Stopped()
        {
        }
    }
}