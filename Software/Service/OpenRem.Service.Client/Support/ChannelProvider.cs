using Grpc.Core;
using OpenRem.Service.Protocol;

namespace OpenRem.Service.Client
{
    class ChannelProvider: IChannelProvider
    {
        public Channel GetChannel()
        {
            return new Channel(ServiceConfig.HostName, ServiceConfig.ServicePort, ChannelCredentials.Insecure);
        }
    }
}