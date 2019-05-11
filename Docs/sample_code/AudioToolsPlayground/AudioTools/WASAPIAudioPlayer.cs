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
        ///     Creates WasapiOut, initializes, wraps it in RawSound, plays it and returns.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="soundConfig">audioDeviceId inside should be provided by MMDeviceEnumerator or its derivatives</param>
        /// <returns></returns>
        public ISound PlaySound(Stream stream, SoundConfig soundConfig)
        {
            var rawWaveStream = new RawSourceWaveStream(stream,
                new WaveFormat(soundConfig.SamplingRate, (int) soundConfig.BitDepth, (int) soundConfig.Channels));

            WasapiOut wasapiOut;
            var deviceId = soundConfig.AudioDeviceId ?? DeviceId;
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

        public ISound PlaySound(byte[] data, SoundConfig soundConfig)
        {
            return PlaySound(new MemoryStream(data), soundConfig);
        }

        public string DeviceId { get; set; }
    }
}