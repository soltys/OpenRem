using System.ServiceModel;
using OpenRem.Service.Interface;

namespace OpenRem.Service
{
    [ServiceContract]
    public interface IOpenRemService
    {
        [OperationContract]
        AnalyzerDTO[] GetAnalyzers();
    }
}
