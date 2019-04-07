using OpenRem.Common;

namespace OpenRem.Config
{
    class ArduinoConfigReader: IArduinoConfigReader
    {
        private string ConfigName => "ArduinoConfig.xml";

        public void GetConfig(string name)
        {
            var configStream  =typeof(ArduinoConfigReader).Assembly.GetResourceStream(ConfigName);
            var arduinoList = ArduinoList.DeserializeFrom(configStream);
            
        }
    }
}
