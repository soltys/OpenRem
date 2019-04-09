using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenRem.Config
{
    public class ArduinoConfig
    {
        public string Name { get; set; }
        public  int SampleRate { get; set; }
        public int BitRate { get; set; }
        public int ChannelsNumber { get; set; }
        public ProbeConfig[] Probes { get; set; }
    }
}
