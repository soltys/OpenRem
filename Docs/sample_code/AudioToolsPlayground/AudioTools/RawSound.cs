using System;
using AudioTools.Interface;
using NAudio.Wave;

namespace AudioTools
{
    public class RawSound : ISound
    {
        private readonly IWavePlayer _wavePlayer;

        public RawSound(IWavePlayer wavePlayer)
        {
            _wavePlayer = wavePlayer;
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
            _wavePlayer.Pause();
            return this;
        }

        public ISound Play()
        {
            _wavePlayer.Play();
            return this;
        }

        public ISound Stop()
        {
            _wavePlayer.Stop();
            return this;
        }

        /// <summary>
        /// public delegate void EventHandler<TEventArgs>(object sender, TEventArgs e);
        /// </summary>
        public event EventHandler<EventArgs> PlaybackFinished
        {
            add => _wavePlayer.PlaybackStopped += new EventHandler<StoppedEventArgs>(value);
            remove => _wavePlayer.PlaybackStopped -= new EventHandler<StoppedEventArgs>(value);
        }

        public void Dispose()
        {
            _wavePlayer?.Dispose();
        }
    }
}