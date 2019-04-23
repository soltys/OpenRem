using System.Linq;
using OpenRem.Common.Config;
using OpenRem.Config.Infrastructure;
using OpenRem.Config.Model.AnalyzerCollection;

namespace OpenRem.Config.Reader
{
    class AnalyzerCollectionConfigReader : IAnalyzerConfigReader
    {
        private IBusinessLogicConfiguration configuration;

        public AnalyzerCollectionConfigReader(IBusinessLogicConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public AnalyzerConfig GetConfig(string name)
        {
            var dtos = this.configuration.BindAll<AnalyzerDto>("AnalyzerCollection");

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