using System;
using System.IO;
using System.Linq;
using AudioTools.Interface;
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
        /// Plays PCM sound
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="samplingRate"></param>
        /// <param name="bitDepth"></param>
        /// <param name="channels"></param>
        /// <param name="audioDeviceId">It should be provided by WaveOut Capabilities (ProductGuid)</param>
        /// <returns></returns>
        public ISound PlaySound(Stream stream, int samplingRate, BitDepth bitDepth, Channels channels,
            string audioDeviceId = null)
        {
            // create WaveOutEvent and Init using NAudio
            var rawWaveStream = new RawSourceWaveStream(stream, new WaveFormat(samplingRate, (int) bitDepth, (int) channels));
            var waveOutEvent = new WaveOutEvent();

            if (audioDeviceId != null)
                waveOutEvent.DeviceNumber = DeviceIdToNumber(audioDeviceId);
            else if (DeviceId != null)
                waveOutEvent.DeviceNumber = DeviceIdToNumber(DeviceId);
            waveOutEvent.Init(rawWaveStream);

            // wrap into local sound implementation
            var sound = new RawSound(waveOutEvent);
            sound.Play();
            return sound;
        }

        /// <summary>
        /// Plays PCM sound
        /// </summary>
        /// <param name="data"></param>
        /// <param name="samplingRate"></param>
        /// <param name="bitDepth"></param>
        /// <param name="channels"></param>
        /// <param name="audioDeviceId">It should be provided by WaveOut Capabilities (ProductGuid)</param>
        /// <param name="stream"></param>
        /// <returns></returns>
        public ISound PlaySound(byte[] data, int samplingRate, BitDepth bitDepth, Channels channels,
            string audioDeviceId = null)
        {
            return PlaySound(new MemoryStream(data), samplingRate, bitDepth, channels, audioDeviceId);
        }

        /// <summary>
        /// Compatible with ProductGuid from WaveOut Capabilities
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