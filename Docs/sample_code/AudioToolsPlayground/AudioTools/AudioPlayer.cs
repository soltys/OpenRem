using System.IO;
using System.Threading;
using AudioTools.Interface;
using NAudio.Wave;

namespace AudioTools
{
    public class AudioPlayer : IAudioPlayer
    {
        public void PlaySound(ISound sound)
        {
            throw new System.NotImplementedException();
        }

        // TODO: Make a separate thread
        public void PlaySound(Stream stream, int samplingRate, BitDepth bitDepth, Channels channels)
        {
            var rawWaveStream = new RawSourceWaveStream(stream, new WaveFormat(samplingRate, (int)bitDepth, (int)channels));
            var waveOutEvent = new WaveOutEvent();
            waveOutEvent.Init(rawWaveStream);
            waveOutEvent.Play();
            while (waveOutEvent.PlaybackState == PlaybackState.Playing)
            {
                Thread.Sleep(500);
            }
            waveOutEvent.Dispose();
        }
        
        public void PlaySound(byte[] data, int samplingRate, BitDepth bitDepth, Channels channels)
        {
            PlaySound(new MemoryStream(data), samplingRate, bitDepth, channels);
        }
    }
}