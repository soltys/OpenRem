using OpenRem.Common;

namespace OpenRem.Engine.Interface
{
    public class Analyzer
    {
        public Analyzer(IDataStream dataStream, string name)
        {
            DataStream = dataStream;
            Name = name;
        }

        public IDataStream DataStream { get; }
        public string Name { get; set; }
    }
}