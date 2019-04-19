using Grpc.Core;

namespace OpenRem.Service.Client
{
    interface IChannelProvider
    {
        Channel GetChannel();
    }
}