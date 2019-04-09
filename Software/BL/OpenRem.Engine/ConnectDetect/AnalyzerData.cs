using System;
using OpenRem.Config;
using OpenRem.HAL;

namespace OpenRem.Engine
{
    internal class AnalyzerData
    {
        public Func<IDataStream> FactoryMethod { get; set; }
        public AnalyzerConfig AnalyzerConfig { get; set; }
    }
}