using System;
using OpenRem.Engine;

namespace OpenRem.Service.Interface
{
    public interface IServiceWrapper
    {
        void StartServer();
        void StopServerIfInternalInstance();
        bool IsRunning { get; }

        Analyzer[] GetAnalyzers();

        void StartRawFileRecorder(Guid analyzerGuid, string fileName);

        void StopRawFileRecorder();
    }
}
