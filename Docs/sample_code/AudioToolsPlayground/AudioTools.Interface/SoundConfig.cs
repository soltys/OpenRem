namespace AudioTools.Interface
{
    public class SoundConfig
    {
        public SoundConfig(int samplingRate, BitDepth bitDepth, Channels channels, string audioDeviceId = null)
        {
            SamplingRate = samplingRate;
            BitDepth = bitDepth;
            Channels = channels;
            AudioDeviceId = audioDeviceId;
        }

        public int SamplingRate { get; private set; }
        public BitDepth BitDepth { get; private set; }
        public Channels Channels { get; private set; }
        public string AudioDeviceId { get; private set; }
    }
}