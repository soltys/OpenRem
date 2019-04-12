using System;
using System.ServiceModel;

namespace OpenRem.Engine
{
    [ServiceContract]
    public interface IRawFileRecorder
    {
        [OperationContract]
        void Start(Guid analyzerGuid, string fileName);
        [OperationContract]
        void Stop();
    }
}
