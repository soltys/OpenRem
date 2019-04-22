using System;
using System.Threading.Tasks;
using OpenRem.Engine;
using OpenRem.Service.Protocol;

namespace OpenRem.Service.Client
{
    class RawFileRecorderClient : RpcClient<RawFileRecorder.RawFileRecorderClient>, IRawFileRecorder
    {
        public RawFileRecorderClient(IChannelProvider channelProvider) : base(channelProvider)
        {
            this.Client = new RawFileRecorder.RawFileRecorderClient(this.Channel);
        }

        public async Task StartAsync(Guid analyzerGuid, string fileName)
        {
            await this.Client.StartAsync(new StartRecordingRequest()
            {
                Id = analyzerGuid.ToString(),
                FileName = fileName
            });
        }

        public async Task StopAsync()
        {
            await this.Client.StopAsync(new EmptyRequest());
        }
    }
}