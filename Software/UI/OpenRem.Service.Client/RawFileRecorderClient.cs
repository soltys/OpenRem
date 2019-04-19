using System;
using System.Threading.Tasks;
using OpenRem.Engine;
using OpenRem.Service.Protocol;

namespace OpenRem.Service.Client
{
    public class RawFileRecorderClient: RawFileRecorder.RawFileRecorderClient, IRawFileRecorder
    {
        public async Task StartAsync(Guid analyzerGuid, string fileName)
        {
            await StartAsync(new StartRecordingRequest()
            {
                Id = analyzerGuid.ToString(),
                FileName = fileName
            });
        }

        public async Task  StopAsync()
        {
           await StopAsync(new EmptyRequest());
        }
    }
}
