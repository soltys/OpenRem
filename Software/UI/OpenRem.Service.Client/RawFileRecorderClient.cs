using System;
using OpenRem.Engine;
using OpenRem.Service.Protocol;

namespace OpenRem.Service.Client
{
    public class RawFileRecorderClient: RawFileRecorder.RawFileRecorderClient, IRawFileRecorder
    {
        public void Start(Guid analyzerGuid, string fileName)
        {
            this.Start(new StartRecordingRequest()
            {
                Id = analyzerGuid.ToString(),
                FileName = fileName
            });
        }

        public void Stop()
        {
           this.Stop(new EmptyRequest());
        }
    }
}
