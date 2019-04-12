using System;

namespace OpenRem.Engine
{
    internal static class PcmEncodingHelper
    {
        public static int ToByteLength(PcmEncoding pcmEncoding)
        {
            return (int) pcmEncoding / 8;
        }

        public static PcmEncoding ToPcmEncoding(int subchunkSize)
        {
            switch (subchunkSize)
            {
                case 32:
                    return PcmEncoding.PCM32Bit;
                case 16:
                    return PcmEncoding.PCM16Bit;
                case 8:
                    return PcmEncoding.PCM8Bit;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}