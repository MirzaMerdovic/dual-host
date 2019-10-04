using System.Threading.Tasks;

namespace ConsoleOut.DualHost.AspNet30.Services
{
    public class MyHealthService : IHealthService
    {
        public Task<bool> IsHealthy()
        {
            return Task.FromResult(true);
        }
    }
}