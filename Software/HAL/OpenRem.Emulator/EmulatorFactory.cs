using OpenRem.HAL;

namespace OpenRem.Emulator
{
    class EmulatorFactory : IEmulatorFactory
    {
        public IDataStream Create(string fileName)
        {
            return new InfiniteFileDataStream(fileName);
        }
    }
}
