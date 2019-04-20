using System.ServiceModel;
using System.Threading.Tasks;

namespace OpenRem.Engine
{
    public interface IDetectManager
    {
        Task<Analyzer[]> GetAnalyzersAsync();
    }
}