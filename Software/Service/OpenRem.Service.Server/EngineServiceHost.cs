using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using Grpc.Core;
using OpenRem.Engine;
using OpenRem.Service.Interface;
using OpenRem.Service.Protocol;

namespace OpenRem.Service
{
    public class EngineServiceHost : IEngineServiceHost
    {
        private Server server;
        private ILifetimeScope scope;

        public EngineServiceHost(ILifetimeScope scope)
        {
            this.scope = scope;
        }

        public void Start()
        {
            this.server = new Server
            {
                Services =
                {
                    DetectManager.BindService(new DetectManagerImpl(this.scope.Resolve<IDetectManager>())),
                    RawFileRecorder.BindService(new RawFileRecorderImpl(this.scope.Resolve<IRawFileRecorder>()))
                },
                Ports = { new ServerPort(ServiceConfig.HostName, ServiceConfig.ServicePort, ServerCredentials.Insecure) }
            };
            this.server.Start();
        }

        public async Task StopAsync()
        {
            await this.server.ShutdownAsync().ConfigureAwait(false);
        }
    }
}
