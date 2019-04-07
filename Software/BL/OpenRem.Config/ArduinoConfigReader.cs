using System.Linq;
using OpenRem.Config.ConfigFiles;

namespace OpenRem.Config
{
    class ArduinoConfigReader : IArduinoConfigReader
    {
        private readonly IEmbeddedConfig embeddedConfig;
        private string ConfigName => "ArduinoConfig.xml";

        public ArduinoConfigReader(IEmbeddedConfig embeddedConfig)
        {
            this.embeddedConfig = embeddedConfig;
        }

        public ArduinoConfig GetConfig(string name)
        {
            var configFile = this.embeddedConfig.GetConfigFile(ConfigName);
            var arduinoList = ArduinoList.DeserializeFrom(configFile);
            var arduinoConfig = arduinoList.Arduino.Single(x => x.Name == name);
            return new ArduinoConfig
            {
                Name = arduinoConfig.Name,
                BitRate = int.Parse(arduinoConfig.BitRate),
                ChannelsNumber = int.Parse(arduinoConfig.ChannelsNumber),
                SampleRate = int.Parse(arduinoConfig.SampleRate),
                Probes = arduinoConfig.Probe.Select(probe => new ProbeConfig()
                {
                    Side = probe.Side.ToSide(),
                    InputChannel = int.Parse(probe.Input.Channel),
                    OutputChannel = int.Parse(probe.Output.Channel)
                }).ToArray()
            };
        }
    }
}
