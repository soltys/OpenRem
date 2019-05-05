namespace AudioTools.Interface.DeviceDetection
{
    public interface IAudioDevice
    {
        string Name { get; }

        DeviceType DeviceType { get; }

        string ID { get; }
    }
}