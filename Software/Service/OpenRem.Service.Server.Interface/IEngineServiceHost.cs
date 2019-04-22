using System.Threading.Tasks;

namespace OpenRem.Service.Server
{
    public interface IEngineServiceHost
    {
        void Start();
        Task StopAsync();

        string HostName { get; }
        int Port { get; }
    }
}