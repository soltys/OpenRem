using System;
using OpenRem.Config;
using OpenRem.HAL;

namespace OpenRem.Engine
{
    internal class AnalyzerData: IEquatable<AnalyzerData>
    {
        public AnalyzerData(Guid id, HardwareKey hardwareKey)
        {
            Id = id;
            HardwareKey = hardwareKey ?? throw new ArgumentNullException(nameof(hardwareKey));
        }

        /// <summary>
        /// Id for retrieving AnalyzerData from collection
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Key which tells if hardware is the same
        /// </summary>
        public HardwareKey HardwareKey { get; }

        /// <summary>
        /// Method to access data from analyzer
        /// </summary>
        public Func<IDataStream> Factory { get; set; }

        /// <summary>
        /// Config found for analyzer
        /// </summary>
        public AnalyzerConfig AnalyzerConfig { get; set; }

        public bool Equals(AnalyzerData other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(HardwareKey, other.HardwareKey);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AnalyzerData) obj);
        }

        public override int GetHashCode()
        {
            return (HardwareKey != null ? HardwareKey.GetHashCode() : 0);
        }
    }
}