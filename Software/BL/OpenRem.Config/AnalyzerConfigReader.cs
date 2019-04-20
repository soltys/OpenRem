using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using OpenRem.Common.Config;
namespace OpenRem.Config
{
    class AnalyzerConfigReader : IAnalyzerConfigReader
    {
        private IConfiguration configuration;

        public AnalyzerConfigReader(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public AnalyzerConfig GetConfig(string name)
        {            
            var dtos = configuration.BindAll<AnalyzerDto>("AnalyzerCollection");

            var dto = dtos.FirstOrDefault(x => x.Name == name);
            if (dto == null)
            {
                throw new ConfigNotFoundException($"Requested {name}");
            }

            return new AnalyzerConfig
            {
                Name = dto.Name,
                SubChunkSize = dto.SubChunkSize,
                ChannelsNumber = dto.Channels,
                SampleRate = dto.SampleRate,
                Probes = dto.Probes.Select(probe => new ProbeConfig()
                {
                    Side = probe.Side,
                    InputChannel = probe.Input.Channel,
                    OutputChannel = probe.Output.Channel
                }).ToArray()
            };
        }

        
    }
}