using System;

namespace AudioTools.Interface
{
    public interface ISound : IDisposable
    {
        event EventHandler<EventArgs> PlaybackFinished;
        
        ISound AddSample(byte[] sample);
        ISound Pause();
        ISound Play();
        ISound Stop();
    }
}