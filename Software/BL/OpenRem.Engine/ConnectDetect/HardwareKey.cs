using System;

namespace OpenRem.Engine
{
    internal class HardwareKey: IEquatable<HardwareKey>
    {
        public HardwareKey(string hardwareName)
        {
            HardwareName = hardwareName;
        }

        public string HardwareName { get;  }

        public bool Equals(HardwareKey other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(HardwareName, other.HardwareName);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((HardwareKey) obj);
        }

        public override int GetHashCode()
        {
            return (HardwareName != null ? HardwareName.GetHashCode() : 0);
        }
    }
}