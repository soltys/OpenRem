using OpenRem.Common;

namespace OpenRem.Emulator.Interface
{
    public interface IEmulatorFactory
    {
        IDataStream Create(string fileName);
    }
}