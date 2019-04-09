namespace OpenRem.Engine
{
    internal static class PcmEncodingHelper
    {
        public static int ToByteLength(PcmEncoding pcmEncoding)
        {
            return (int) pcmEncoding / 8;
        }
    }
}