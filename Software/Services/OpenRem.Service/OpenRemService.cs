using System;
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
        private readonly IRawFileRecorder rawFileRecorder;

        public OpenRemService(IDetectManager detectManager, IRawFileRecorder rawFileRecorder)
        {
            this.detectManager = detectManager;
            this.rawFileRecorder = rawFileRecorder;
        }

        public AnalyzerDTO[] GetAnalyzers()
        {
            return this.detectManager.GetAnalyzers().Select(x => new AnalyzerDTO() { Id = x.Id, Name = x.Name }).ToArray();
        }

        public void StartRawFileRecorder(Guid analyzerGuid, string fileName)
        {
            this.rawFileRecorder.Start(analyzerGuid, fileName);
        }

        public void StopRawFileRecorder()
        {
            this.rawFileRecorder.Stop();
        }
    }
}
