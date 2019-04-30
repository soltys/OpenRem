using Grpc.Core;
using OpenRem.Common.Service;

namespace OpenRem.Service.Client
{
    class ChannelProvider : IChannelProvider
    {
        private readonly ServiceConfig config;

        public ChannelProvider(ServiceConfig config)
        {
            this.config = config;
        }

        public Channel GetChannel()
        {
            return new Channel(this.config.HostName, this.config.ServicePort, ChannelCredentials.Insecure);
        }
    }
}