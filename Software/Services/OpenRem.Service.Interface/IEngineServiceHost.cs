using System.Threading.Tasks;

namespace OpenRem.Service.Interface
{
    public interface IEngineServiceHost
    {
        void Start();
        Task Stop();
    }
}
