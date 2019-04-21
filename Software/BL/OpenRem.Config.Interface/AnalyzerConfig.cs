namespace OpenRem.Config
{
    public class AnalyzerConfig
    {
        public string Name { get; set; }
        public  int SampleRate { get; set; }
        public int SubChunkSize { get; set; }
        public int ChannelsNumber { get; set; }
        public ProbeConfig[] Probes { get; set; }
    }
}
