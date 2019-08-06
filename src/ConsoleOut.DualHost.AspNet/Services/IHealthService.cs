using System.Threading.Tasks;

namespace ConsoleOut.DualHost.AspNet.Services
{
    public interface IHealthService
    {
        Task<bool> IsHealthy();
    }
}