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

        public ISound AddSample(byte[] sample)
        {
            throw new NotImplementedException();
        }

        public ISound Pause()
        {
            _waveOutEvent.Pause();
            return this;
        }

        public ISound Play()
        {
            _waveOutEvent.Play();
            return this;
        }

        public ISound Stop()
        {
            _waveOutEvent.Stop();
            return this;
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