using System;
using System.IO;
using AudioTools.Interface;
using NAudio.CoreAudioApi;
using NAudio.Wave;

namespace AudioTools
{
    public class WASAPIAudioPlayer : IAudioPlayer
    {
        public ISound PlaySound(ISound sound)
        {
            throw new NotImplementedException();
        }

        public ISound PlaySound(Stream stream, int samplingRate, BitDepth bitDepth, Channels channels,
            string audioDeviceId = null)
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

        public ISound PlaySound(byte[] data, int samplingRate, BitDepth bitDepth, Channels channels,
            string audioDeviceId = null)
        {
            return PlaySound(new MemoryStream(data), samplingRate, bitDepth, channels, audioDeviceId);
        }

        public string DeviceId { get; set; }
    }
}