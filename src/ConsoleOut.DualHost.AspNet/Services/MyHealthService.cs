using System.Threading.Tasks;

namespace ConsoleOut.DualHost.AspNet.Services
{
    public class MyHealthService : IHealthService
    {
        public Task<bool> IsHealthy()
        {
            return Task.FromResult(true);
        }
    }
}