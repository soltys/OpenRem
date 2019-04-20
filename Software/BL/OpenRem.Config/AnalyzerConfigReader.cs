using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace OpenRem.Config
{
    class AnalyzerConfigReader : IAnalyzerConfigReader
    {
        private IConfigurationRoot configurationRoot;

        public AnalyzerConfigReader(IConfigurationRoot configurationRoot)
        {
            this.configurationRoot = configurationRoot;
        }

        public AnalyzerConfig GetConfig(string name)
        {            
            var dtos = ExtractConfig<AnalyzerDto>(configurationRoot, "AnalyzerCollection");

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

        private static List<T> ExtractConfig<T>(IConfigurationRoot config, string sectionName) where T : class, new()
        {
            var entries = config.GetSection(sectionName)
                                 .GetChildren();

            var dtos = new List<T>(entries.Count());
            foreach (var entry in entries)
            {
                var dto = new T();
                entry.Bind(dto);
                dtos.Add(dto);
            }

            return dtos;
        }
    }
}