using OpenRem.Common;
using OpenRem.Emulator.Interface;

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
