﻿using System.IO;

namespace AudioTools.Interface
{
    public interface IAudioPlayer
    {
        /// <summary>
        /// Plays a sound using dedicated interface
        /// </summary>
        /// <param name="sound"></param>
        /// <returns>Sound instance</returns>
        ISound PlaySound(ISound sound);

        /// <summary>
        /// Plays a sound using stream
        /// </summary>
        /// <param name="stream">More in MSDN</param>
        /// <param name="samplingRate">In other words - Samples per second</param>
        /// <param name="bitDepth">Bits per sample</param>
        /// <param name="channels">Mono/Stereo</param>
        /// <returns>Sound instance</returns>
        ISound PlaySound(Stream stream, int samplingRate, BitDepth bitDepth, Channels channels);

        /// <summary>
        /// Plays a sound using byte array
        /// </summary>
        /// <param name="data"></param>
        /// <param name="samplingRate">In other words - Samples per second</param>
        /// <param name="bitDepth">Bits per sample</param>
        /// <param name="channels">Mono/Stereo</param>
        /// <returns>Sound instance</returns>
        ISound PlaySound(byte[] data, int samplingRate, BitDepth bitDepth, Channels channels);
    }
}