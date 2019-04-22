using System.Threading.Tasks;
using Autofac;
using Grpc.Core;
using OpenRem.Engine;
using OpenRem.Service.Protocol;

namespace OpenRem.Service.Server
{
    public class EngineServiceHost : IEngineServiceHost
    {
        private Grpc.Core.Server server;
        private ILifetimeScope scope;
        private readonly ServiceConfig config;

        public EngineServiceHost(ILifetimeScope scope)
        {
            this.scope = scope;
            this.config = this.scope.Resolve<ServiceConfig>();
        }

        public string HostName => this.config.HostName;

        public int Port => this.config.ServicePort;

        public void Start()
        {
            this.server = new Grpc.Core.Server
            {
                Services =
                {
                    DetectManager.BindService(new DetectManagerImpl(this.scope.Resolve<IDetectManager>())),
                    RawFileRecorder.BindService(new RawFileRecorderImpl(this.scope.Resolve<IRawFileRecorder>()))
                },
                Ports = {new ServerPort(config.HostName, config.ServicePort, ServerCredentials.Insecure)}
            };
            this.server.Start();
        }

        public async Task StopAsync()
        {
            await this.server.ShutdownAsync().ConfigureAwait(false);
        }
    }
}