using System;
using System.ServiceModel;
using OpenRem.Service.Interface;

namespace OpenRem.Service
{
    [ServiceContract]
    public interface IOpenRemService
    {
        [OperationContract]
        AnalyzerDTO[] GetAnalyzers();

        [OperationContract]
        void StartRawFileRecorder(Guid analyzerGuid, string fileName);

        [OperationContract]
        void StopRawFileRecorder();
    }
}
