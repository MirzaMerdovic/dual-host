using System.Threading.Tasks;

namespace ConsoleOut.DualHost.AspNet30.Services
{
    public interface IHealthService
    {
        Task<bool> IsHealthy();
    }
}