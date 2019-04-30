using Grpc.Core;

namespace OpenRem.Common.Service
{
    public interface IChannelProvider
    {
        Channel GetChannel();
    }
}