using System;
using System.Threading.Tasks;
using Grpc.Core;
using OpenRem.Engine;
using OpenRem.Service.Protocol;

namespace OpenRem.Service
{
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
}