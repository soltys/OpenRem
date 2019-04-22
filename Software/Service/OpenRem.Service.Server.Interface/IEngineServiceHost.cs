using System.Threading.Tasks;

namespace OpenRem.Service.Interface
{
    public interface IEngineServiceHost
    {
        void Start();
        Task StopAsync();

        string HostName { get; }
        int Port { get; }
    }
}
