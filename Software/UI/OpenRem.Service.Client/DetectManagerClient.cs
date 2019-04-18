using System;
using System.Linq;
using Grpc.Core;
using OpenRem.Engine;
using OpenRem.Service.Protocol;

namespace OpenRem.Service.Client
{
    public class DetectManagerClient : DetectManager.DetectManagerClient, IDetectManager
    {
        public Analyzer[] GetAnalyzers()
        {
            var response = this.GetAnalyzers(new EmptyRequest(), new CallOptions());
            return response.Analyzers.Select(x => new Analyzer
            {
                Id = Guid.Parse(x.Id),
                Name = x.Name
            }).ToArray();
        }
    }
}