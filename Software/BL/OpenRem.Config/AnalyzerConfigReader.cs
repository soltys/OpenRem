using System.Linq;

namespace OpenRem.Config
{
    class AnalyzerConfigReader : IAnalyzerConfigReader
    {
        private readonly IEmbeddedConfig embeddedConfig;
        private string ConfigName => "AnalyzerConfig.xml";

        public AnalyzerConfigReader(IEmbeddedConfig embeddedConfig)
        {
            this.embeddedConfig = embeddedConfig;
        }

        public AnalyzerConfig GetConfig(string name)
        {
            var configFile = this.embeddedConfig.GetConfigFile(ConfigName);
            return new AnalyzerConfig();
            //var arduinoList = AnalyzerList.DeserializeFrom(configFile);
            //var arduinoConfig = arduinoList.Analyzer.Single(x => x.Name == name);
            //return new AnalyzerConfig
            //{
            //    Name = arduinoConfig.Name,
            //    SubChunkSize = int.Parse(arduinoConfig.SubChunkSize),
            //    ChannelsNumber = int.Parse(arduinoConfig.ChannelsNumber),
            //    SampleRate = int.Parse(arduinoConfig.SampleRate),
            //    Probes = arduinoConfig.Probe.Select(probe => new ProbeConfig()
            //    {
            //        Side = probe.Side.ToSide(),
            //        InputChannel = int.Parse(probe.Input.Channel),
            //        OutputChannel = int.Parse(probe.Output.Channel)
            //    }).ToArray()
            //};
        }
    }
}
