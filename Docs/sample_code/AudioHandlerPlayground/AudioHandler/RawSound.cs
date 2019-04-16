using AudioHandler.Interface;

namespace AudioHandler
{
    public class RawSound : ISound
    {
        public RawSound(byte[] data, int bitRate, Channels channels)
        {
            
        }
        
        public void AddSample(byte[] sample)
        {
            throw new System.NotImplementedException();
        }
    }
}