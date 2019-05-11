namespace AudioTools.Interface
{
    public class SoundConfig
    {
        /// <summary>
        ///     General config far PCM like (raw sound)
        /// </summary>
        /// <param name="samplingRate">In other words - Samples per second</param>
        /// <param name="bitDepth">Bits per sample</param>
        /// <param name="channels">Mono/Stereo</param>
        /// <param name="audioDeviceId">Id might differ for different detection managers (MM, WaveOut, etc..)</param>
        public SoundConfig(int samplingRate, BitDepth bitDepth, Channels channels, string audioDeviceId = null)
        {
            SamplingRate = samplingRate;
            BitDepth = bitDepth;
            Channels = channels;
            AudioDeviceId = audioDeviceId;
        }

        public int SamplingRate { get; }
        public BitDepth BitDepth { get; }
        public Channels Channels { get; }
        public string AudioDeviceId { get; }
    }
}