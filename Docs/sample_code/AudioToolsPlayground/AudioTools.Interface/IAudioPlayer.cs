using System.IO;
using AudioTools.Interface.Config;

namespace AudioTools.Interface
{
    public interface IAudioPlayer
    {
        string DeviceId { get; set; }

        /// <summary>
        ///     Plays a sound using dedicated interface
        /// </summary>
        /// <param name="sound"></param>
        /// <returns>Sound instance</returns>
        ISound PlaySound(ISound sound);

        /// <summary>
        ///     Plays a sound using stream
        /// </summary>
        /// <param name="stream">More in MSDN</param>
        /// <param name="soundConfig"></param>
        /// <returns>Sound instance</returns>
        ISound PlaySound(Stream stream, SoundConfig soundConfig);

        /// <summary>
        ///     Plays a sound using byte array
        /// </summary>
        /// <param name="data"></param>
        /// <param name="soundConfig"></param>
        /// <returns>Sound instance</returns>
        ISound PlaySound(byte[] data, SoundConfig soundConfig);
    }
}