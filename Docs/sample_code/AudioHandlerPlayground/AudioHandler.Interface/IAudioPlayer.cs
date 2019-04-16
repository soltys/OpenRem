using System.IO;

namespace AudioHandler.Interface
{
    public interface IAudioPlayer
    {
        /// <summary>
        /// Plays a sound using dedicated interface
        /// </summary>
        /// <param name="sound"></param>
        void PlaySound(ISound sound);

        /// <summary>
        /// Plays a sound using stream
        /// </summary>
        /// <param name="stream">More in MSDN</param>
        /// <param name="samplingRate">In other words - Samples per second</param>
        /// <param name="bitDepth">Bits per sample</param>
        /// <param name="channels">Mono/Stereo</param>
        void PlaySound(Stream stream, int samplingRate, BitDepth bitDepth, Channels channels);
        
        /// <summary>
        /// Plays a sound using byte array
        /// </summary>
        /// <param name="data"></param>
        /// <param name="samplingRate">In other words - Samples per second</param>
        /// <param name="bitDepth">Bits per sample</param>
        /// <param name="channels">Mono/Stereo</param>
        void PlaySound(byte[] data, int samplingRate, BitDepth bitDepth, Channels channels);
    }
}