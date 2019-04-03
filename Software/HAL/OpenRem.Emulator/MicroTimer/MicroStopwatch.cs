using System;
using System.Diagnostics;

namespace OpenRem.Emulator
{
    /// <summary>
    /// MicroStopwatch class
    /// </summary>
    public class MicroStopwatch : System.Diagnostics.Stopwatch
    {
        readonly double _microSecPerTick = 1000000D / Stopwatch.Frequency;

        public MicroStopwatch()
        {
            if (!Stopwatch.IsHighResolution)
            {
                throw new Exception("On this system the high-resolution " +
                                    "performance counter is not available");
            }
        }

        public long ElapsedMicroseconds => (long)(ElapsedTicks * this._microSecPerTick);
    }
}
