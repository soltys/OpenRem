using System;
using System.IO;
using System.Linq;
using AudioTools.Interface;
using AudioTools.Interface.Config;
using NAudio.Wave;

namespace AudioTools
{
    [Obsolete("Using WASAPI version is recommended.")]
    public class WaveOutAudioPlayer : IAudioPlayer
    {
        public ISound PlaySound(ISound sound)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Plays PCM sound
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="soundConfig">audioDeviceId inside should be provided by WaveOut Capabilities (ProductGuid)</param>
        /// <returns></returns>
        public ISound PlaySound(Stream stream, SoundConfig soundConfig)
        {
            // create WaveOutEvent and Init using NAudio
            var rawWaveStream = new RawSourceWaveStream(stream,
                new WaveFormat(soundConfig.SamplingRate, (int) soundConfig.BitDepth, (int) soundConfig.Channels));
            var waveOutEvent = new WaveOutEvent();

            if (soundConfig.AudioDeviceId != null)
                waveOutEvent.DeviceNumber = DeviceIdToNumber(soundConfig.AudioDeviceId);
            else if (DeviceId != null)
                waveOutEvent.DeviceNumber = DeviceIdToNumber(DeviceId);
            waveOutEvent.Init(rawWaveStream);

            // wrap into local sound implementation
            var sound = new RawSound(waveOutEvent);
            sound.Play();
            return sound;
        }

        /// <summary>
        ///     Plays PCM sound
        /// </summary>
        /// <param name="data"></param>
        /// <param name="soundConfig">audioDeviceId inside should be provided by WaveOut Capabilities (ProductGuid)</param>
        /// <returns></returns>
        public ISound PlaySound(byte[] data, SoundConfig soundConfig)
        {
            return PlaySound(new MemoryStream(data), soundConfig);
        }

        /// <summary>
        ///     Compatible with ProductGuid from WaveOut Capabilities
        /// </summary>
        public string DeviceId { get; set; }

        #region Private helpers

        private int DeviceIdToNumber(string audioDeviceId)
        {
            var devices = Enumerable.Range(0, WaveOut.DeviceCount).Select(i => WaveOut.GetCapabilities(i)).ToList();
            return devices.IndexOf(devices.Single(capabilities => capabilities.ProductGuid.ToString() == audioDeviceId));
        }

        #endregion
    }
}