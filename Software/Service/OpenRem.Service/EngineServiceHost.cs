using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Grpc.Core;
using OpenRem.Engine;
using OpenRem.Service.Interface;
using OpenRem.Service.Protocol;

namespace OpenRem.Service
{

    class Dete2ctManagerImpl : Protocol.DetectManager.DetectManagerClient
    {

    }
    class DetectManagerImpl : Protocol.DetectManager.DetectManagerBase
    {
        private IDetectManager real;

        public DetectManagerImpl(IDetectManager detectManager)
        {
            this.real = detectManager;
        }

        public override async Task<GetAnalyzerResponse> GetAnalyzers(EmptyRequest request, ServerCallContext context)
        {
            var analyzers = await this.real.GetAnalyzersAsync();

            var response = new GetAnalyzerResponse();
            response.Analyzers.AddRange(analyzers.Select(x => new GetAnalyzerResponse.Types.AnalyzerDTO()
            {
                Id = x.Id.ToString(),
                Name = x.Name
            }));
            
            return response;
        }
    }

    class RawFileRecorderImpl: Protocol.RawFileRecorder.RawFileRecorderBase
    {
        private IRawFileRecorder real;

        public RawFileRecorderImpl(IRawFileRecorder detectManager)
        {
            this.real = detectManager;
        }

        public override async Task<EmptyResponse> Start(StartRecordingRequest request, ServerCallContext context)
        {
            await this.real.StartAsync(Guid.Parse(request.Id), request.FileName);
            return new EmptyResponse();
        }

        public override async Task<EmptyResponse> Stop(EmptyRequest request, ServerCallContext context)
        {
            await this.real.StopAsync();
            return new EmptyResponse();
        }
    }


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
                Ports = { new ServerPort("localhost", ServiceConfig.ServicePort, ServerCredentials.Insecure) }
            };
            this.server.Start();
        }

        public async Task StopAsync()
        {
            await this.server.ShutdownAsync().ConfigureAwait(false);
        }
    }
}
