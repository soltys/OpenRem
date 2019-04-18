using System.Threading.Tasks;

namespace OpenRem.Service.Interface
{
    public interface IEngineServceHost
    {
        void Start();
        Task Stop();
    }
}
