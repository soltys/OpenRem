namespace OpenRem.Config
{
    class AnalyzerDto
    {
        public string Name { get; set; }
        public int SampleRate { get; set; }
        public int SubChunkSize { get; set; }
        public int Channels { get; set; }
        public ProbeDto[] Probes { get; set; }
    }
}
