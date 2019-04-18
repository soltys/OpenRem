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
    
    class DetectManagerImpl : Protocol.DetectManager.DetectManagerBase
    {
        private IDetectManager real;

        public DetectManagerImpl(IDetectManager detectManager)
        {
            this.real = detectManager;
        }

        public override Task<GetAnalyzerResponse> GetAnalyzers(EmptyRequest request, ServerCallContext context)
        {
            var analyzers = this.real.GetAnalyzers();

            var response = new GetAnalyzerResponse();
            response.Analyzers.AddRange(analyzers.Select(x => new AnalyzerDto()
            {
                Id = x.Id.ToString(),
                Name = x.Name
            }));
            
            return Task.FromResult(response);
            
        }
    }

    class RawFileRecorderImpl: Protocol.RawFileRecorder.RawFileRecorderBase
    {
        private IRawFileRecorder real;

        public RawFileRecorderImpl(IRawFileRecorder detectManager)
        {
            this.real = detectManager;
        }

        public override Task<EmptyResponse> Start(StartRecordingRequest request, ServerCallContext context)
        {
            this.real.Start(Guid.Parse(request.Id), request.FileName);
            return Task.FromResult(new EmptyResponse());
        }

        public override Task<EmptyResponse> Stop(EmptyRequest request, ServerCallContext context)
        {
            this.real.Stop();
            return Task.FromResult(new EmptyResponse());
        }
    }


    public class EngineServiceHost : IEngineServceHost
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
                    DetectManager.BindService(new DetectManagerImpl(scope.Resolve<IDetectManager>())),
                    RawFileRecorder.BindService(new RawFileRecorderImpl(this.scope.Resolve<IRawFileRecorder>()))
                },
                Ports = { new ServerPort("localhost", OpenRemServiceConfig.ServicePort, ServerCredentials.Insecure) }
            };
            this.server.Start();
        }

        public async Task Stop()
        {
            await this.server.ShutdownAsync().ConfigureAwait(false);
        }

        public static IEnumerable<ServiceType> GetEngineTypes()
        {
            return null;
        }
    }
}
