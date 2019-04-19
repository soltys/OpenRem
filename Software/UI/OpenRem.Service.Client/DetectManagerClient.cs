using System;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using OpenRem.Engine;
using OpenRem.Service.Protocol;

namespace OpenRem.Service.Client
{
    class DetectManagerClient : IDetectManager
    {
        private DetectManager.DetectManagerClient client;

        public DetectManagerClient(IChannelProvider channelProvider)
        {
            this.client = new DetectManager.DetectManagerClient(channelProvider.GetChannel());
        }

        public async Task<Analyzer[]> GetAnalyzersAsync()
        {
            var response = await this.client.GetAnalyzersAsync(new EmptyRequest(), new CallOptions());
            return response.Analyzers.Select(x => new Analyzer
            {
                Id = Guid.Parse(x.Id),
                Name = x.Name
            }).ToArray();
        }
    }
}
