using OpenRem.HAL;

namespace OpenRem.Emulator
{
    public interface IEmulatorFactory
    {
        IDataStream Create(string fileName);
    }
}