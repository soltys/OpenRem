using System;
using AudioTools.Interface;
using NAudio.Wave;

namespace AudioTools
{
    public class RawSound : ISound
    {
        private readonly WaveOutEvent _waveOutEvent;

        public RawSound(WaveOutEvent waveOutEvent)
        {
            _waveOutEvent = waveOutEvent;
        }
        
        private RawSound(byte[] data, int bitRate, Channels channels)
        {
            throw new NotImplementedException();
        }

        public void AddSample(byte[] sample)
        {
            throw new NotImplementedException();
        }

        public void Pause()
        {
            _waveOutEvent.Pause();
        }

        public void Play()
        {
            _waveOutEvent.Play();
        }

        public void Stop()
        {
            _waveOutEvent.Stop();
        }

        /// <summary>
        /// public delegate void EventHandler<TEventArgs>(object sender, TEventArgs e);
        /// </summary>
        public event EventHandler<EventArgs> PlaybackFinished
        {
            add => _waveOutEvent.PlaybackStopped += new EventHandler<StoppedEventArgs>(value);
            remove => _waveOutEvent.PlaybackStopped -= new EventHandler<StoppedEventArgs>(value);
        }
    }
}