namespace AudioTools.Interface.Config
{
    public struct SoundConfig
    {
        public int SamplingRate { get; set; }
        public BitDepth BitDepth { get; set; }
        public Channels Channels { get; set; }
        public float Volume { get; set; }
        public string AudioDeviceId { get; set; }
    }
}