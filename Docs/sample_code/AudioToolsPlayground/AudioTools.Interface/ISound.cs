using System;

namespace AudioTools.Interface
{
    public interface ISound
    {
        event EventHandler<EventArgs> PlaybackFinished;
        
        void AddSample(byte[] sample);
        void Pause();
        void Play();
        void Stop();

    }
}