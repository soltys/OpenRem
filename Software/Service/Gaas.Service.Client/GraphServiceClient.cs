using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gaas.Service.Client.Interface;
using Gaas.Service.Protocol;
using Grpc.Core;
using OpenRem.Common.Service;
using OpenRem.Gaas.Service.Client.Interface;

namespace Gaas.Service.Client
{
    internal class GraphServiceClient : RpcClient<Graph.GraphClient>, IGraphServiceClient
    {
        public GraphServiceClient(IGraphChannelProvider channelProvider) : base(channelProvider)
        {
            this.Client = new Graph.GraphClient(this.Channel);
        }

        public async Task DisplayDataAsync(string name, IEnumerable<DataPoint> dataPoints)
        {

            DisplayDataRequest request = new DisplayDataRequest();
            request.Name = name;
            request.GraphPoints.AddRange(dataPoints.Select(x => new DisplayDataRequest.Types.GraphPoint()
            {
                X = x.X,
                Y = x.Y
            }));

            await Client.DisplayDataAsync(request);
        }
    }

    interface IGraphChannelProvider : IChannelProvider
    {
    }

    class GraphChannelProvider : IGraphChannelProvider
    {
        public Channel GetChannel()
        {
            return new Channel("localhost", 50051, ChannelCredentials.Insecure);
        }
    }
}
