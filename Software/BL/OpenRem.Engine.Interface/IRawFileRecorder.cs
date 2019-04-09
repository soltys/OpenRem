using System;

namespace OpenRem.Engine
{
    public interface IRawFileRecorder
    {
        void Start(Guid analyzerGuid, string fileName);
        void Stop();
    }
}
