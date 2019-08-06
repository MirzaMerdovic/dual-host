using Microsoft.Extensions.Options;
using Nancy.Hosting.Self;
using System;
using System.Collections.Concurrent;

namespace ConsoleOut.DualHost.Hosts
{
    public interface INancyHostFactory
    {
        NancyHost Get();
    }

    public class NancyHostFactory : INancyHostFactory
    {
        private readonly NancySeverOptions _options;

        private readonly ConcurrentDictionary<string, NancyHost> _hosts = new ConcurrentDictionary<string, NancyHost>();

        public NancyHostFactory(IOptions<NancySeverOptions> options)
        {
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
        }

        public NancyHost Get()
        {
            var baseUrl = _options.BaseUrl.EndsWith("/") ? _options.BaseUrl : _options.BaseUrl + "/";

            if (_hosts.ContainsKey(baseUrl))
                return _hosts[baseUrl];

            _hosts[baseUrl] = new NancyHost(new[] { new Uri(baseUrl) });

            return _hosts[baseUrl];
        }
    }
}
