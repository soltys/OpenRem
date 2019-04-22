using NUnit.Framework;

namespace OpenRem.Engine.Test
{
    [TestFixture]
    class PcmEncodingTest
    {
        [TestCase(PcmEncoding.PCM32Bit, 32)]
        [TestCase(PcmEncoding.PCM16Bit, 16)]
        [TestCase(PcmEncoding.PCM8Bit, 8)]
        public void PcmEncoding_IsConvertibleToInt_WithExpectedValues_HowManyBytesPerSample(PcmEncoding encoding,
            int expectedValue)
        {
            Assert.AreEqual(expectedValue, (int) encoding);
        }

        [TestCase(PcmEncoding.PCM32Bit, 4)]
        [TestCase(PcmEncoding.PCM16Bit, 2)]
        [TestCase(PcmEncoding.PCM8Bit, 1)]
        public void PcmEncoding_ToByteSize_InEncoding(PcmEncoding encoding, int expectedValue)
        {
            Assert.AreEqual(expectedValue, PcmEncodingHelper.ToByteLength(encoding));
        }
    }
}