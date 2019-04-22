using OpenRem.Common;

namespace OpenRem.Config.Model.AnalyzerCollection
{
    class ProbeDto
    {
        public Side Side { get; set; }
        public MicrophoneDto Input { get; set; }
        public MicrophoneDto Output { get; set; }
    }
}