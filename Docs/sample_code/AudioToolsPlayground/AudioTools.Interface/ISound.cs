using System;

namespace AudioTools.Interface
{
    public interface ISound
    {
        event EventHandler<EventArgs> PlaybackFinished;
        
        ISound AddSample(byte[] sample);
        ISound Pause();
        ISound Play();
        ISound Stop();

    }
}