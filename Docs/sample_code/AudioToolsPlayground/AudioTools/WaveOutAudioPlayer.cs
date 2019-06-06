using System.IO;
using AudioTools.Interface;
using NAudio.Wave;

namespace AudioTools
{
    public class WaveOutAudioPlayer : IAudioPlayer
    {
        public ISound PlaySound(ISound sound)
        {
            throw new System.NotImplementedException();
        }
        
        public ISound PlaySound(Stream stream, int samplingRate, BitDepth bitDepth, Channels channels)
        {
            // create WaveOutEvent and Init using NAudio
            var rawWaveStream = new RawSourceWaveStream(stream, new WaveFormat(samplingRate, (int)bitDepth, (int)channels));
            var waveOutEvent = new WaveOutEvent();
            waveOutEvent.Init(rawWaveStream);
            
            // wrap into local sound implementation
            var sound = new RawSound(waveOutEvent);
            sound.Play();
            return sound;
        }
        
        public ISound PlaySound(byte[] data, int samplingRate, BitDepth bitDepth, Channels channels)
        {
            return PlaySound(new MemoryStream(data), samplingRate, bitDepth, channels);
        }
    }
}