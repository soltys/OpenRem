using System.Collections.Generic;

namespace OpenRem.Engine.Interface
{
    public interface IDetectManager
    {
        IEnumerable<Analyzer> GetAnalyzers();
    }
}