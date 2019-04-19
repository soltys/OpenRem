using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace OpenRem.Engine
{
    public interface IRawFileRecorder
    {
        Task StartAsync(Guid analyzerGuid, string fileName);
        Task StopAsync();
    }
}
