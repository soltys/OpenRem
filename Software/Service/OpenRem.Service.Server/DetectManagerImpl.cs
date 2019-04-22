using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using OpenRem.Engine;
using OpenRem.Service.Protocol;

namespace OpenRem.Service.Server
{
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
}