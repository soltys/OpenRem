using System;
using System.Threading.Tasks;
using OpenRem.Engine;
using OpenRem.Service.Protocol;

namespace OpenRem.Service.Client
{
    class RawFileRecorderClient: IRawFileRecorder
    {
        private readonly RawFileRecorder.RawFileRecorderClient client;

        public RawFileRecorderClient(IChannelProvider channelProvider)
        {
            this.client = new RawFileRecorder.RawFileRecorderClient(channelProvider.GetChannel());
        }

        public async Task StartAsync(Guid analyzerGuid, string fileName)
        {
            await this.client.StartAsync(new StartRecordingRequest()
            {
                Id = analyzerGuid.ToString(),
                FileName = fileName
            });
        }

        public async Task  StopAsync()
        {
           await this.client.StopAsync(new EmptyRequest());
        }
    }
}
