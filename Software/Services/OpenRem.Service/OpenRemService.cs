using System.Linq;
using System.ServiceModel;
using OpenRem.Engine;
using OpenRem.Service.Interface;

namespace OpenRem.Service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, IncludeExceptionDetailInFaults = true)]
    public class OpenRemService : IOpenRemService
    {
        private readonly IDetectManager detectManager;

        public OpenRemService(IDetectManager detectManager)
        {
            this.detectManager = detectManager;
        }

        public AnalyzerDTO[] GetAnalyzers()
        {
            return this.detectManager.GetAnalyzers().Select(x => new AnalyzerDTO() { Id = x.Id, Name = x.Name }).ToArray();
        }
    }
}
