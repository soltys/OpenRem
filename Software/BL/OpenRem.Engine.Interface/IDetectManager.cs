using System.ServiceModel;

namespace OpenRem.Engine
{
    [ServiceContract]
    public interface IDetectManager
    {
        [OperationContract]
        Analyzer[] GetAnalyzers();
    }
}