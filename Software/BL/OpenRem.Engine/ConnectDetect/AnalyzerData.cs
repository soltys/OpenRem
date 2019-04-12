using System;
using OpenRem.Config;
using OpenRem.HAL;

namespace OpenRem.Engine
{
    internal class AnalyzerData
    {
        public Func<IDataStream> Factory { get; set; }
        public AnalyzerConfig AnalyzerConfig { get; set; }
    }
}