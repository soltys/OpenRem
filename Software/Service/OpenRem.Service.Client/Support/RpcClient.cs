using Grpc.Core;

namespace OpenRem.Service.Client
{
    internal class RpcClient<TRpcClient>
    {
        protected TRpcClient Client;
        protected readonly Channel Channel;

        public RpcClient(IChannelProvider channelProvider)
        {
            this.Channel = channelProvider.GetChannel();
        }
    }
}