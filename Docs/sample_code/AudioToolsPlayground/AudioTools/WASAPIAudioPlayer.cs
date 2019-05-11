using System;
using System.IO;
using AudioTools.Interface;
using NAudio.CoreAudioApi;
using NAudio.Wave;

namespace AudioTools
{
    public class WasapiAudioPlayer : IAudioPlayer
    {
        public ISound PlaySound(ISound sound)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates WasapiOut, initializes, wraps it in RawSound, plays it and returns.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="samplingRate"></param>
        /// <param name="bitDepth"></param>
        /// <param name="channels"></param>
        /// <param name="audioDeviceId">ID should be provided by MMDeviceEnumerator or its derivatives</param>
        /// <returns></returns>
        public ISound PlaySound(Stream stream, int samplingRate, BitDepth bitDepth, Channels channels, string audioDeviceId = null)
        {
            var rawWaveStream = new RawSourceWaveStream(stream, new WaveFormat(samplingRate, (int) bitDepth, (int) channels));

            WasapiOut wasapiOut;
            var deviceId = audioDeviceId != null ? audioDeviceId : DeviceId;
            if (deviceId != null)
                wasapiOut = new WasapiOut(new MMDeviceEnumerator().GetDevice(deviceId), AudioClientShareMode.Shared,
                    false, 300);
            else
                wasapiOut = new WasapiOut(AudioClientShareMode.Shared, false, 300);
            wasapiOut.Init(rawWaveStream);

            var sound = new RawSound(wasapiOut);
            sound.Play();
            return sound;
        }

        public ISound PlaySound(byte[] data, int samplingRate, BitDepth bitDepth, Channels channels, string audioDeviceId = null)
        {
            return PlaySound(new MemoryStream(data), samplingRate, bitDepth, channels, audioDeviceId);
        }

        public string DeviceId { get; set; }
    }
}