using System;
using NUnit.Framework;
using OpenRem.Common;

namespace OpenRem.Engine.Test
{
    [TestFixture]
    public class RawAudioSampleTest
    {
        [TestCase(new byte[] {1, 2}, PcmEncoding.PCM8Bit, Side.Left, new byte[] {1})]
        [TestCase(new byte[] {1, 2}, PcmEncoding.PCM8Bit, Side.Right, new byte[] {2})]
        [TestCase(new byte[] {1, 2, 3, 4}, PcmEncoding.PCM16Bit, Side.Left, new byte[] {1, 2})]
        [TestCase(new byte[] {1, 2, 3, 4}, PcmEncoding.PCM16Bit, Side.Right, new byte[] {3, 4})]
        [TestCase(new byte[] {1, 2, 3, 4, 5, 6, 7, 8}, PcmEncoding.PCM32Bit, Side.Left, new byte[] {1, 2, 3, 4})]
        [TestCase(new byte[] {1, 2, 3, 4, 5, 6, 7, 8}, PcmEncoding.PCM32Bit, Side.Right, new byte[] {5, 6, 7, 8})]
        public void ToMono(byte[] data, PcmEncoding encoding, Side side, byte[] expectedOutput)
        {
            var audioSample = new AudioSample(data, encoding, 2);

            var monoSample = audioSample.ToMono(side);

            CollectionAssert.AreEqual(expectedOutput, monoSample.RawData);
            Assert.AreEqual(encoding, monoSample.PcmEncoding);
            Assert.AreEqual(1, monoSample.ChannelsCount);
        }

        [Test]
        public void ToMono_Exception_OnTooLowChannelsCount()
        {
            var audioSample = new AudioSample(new byte[] {1, 2}, PcmEncoding.PCM16Bit, 1);
            Assert.Throws<InvalidOperationException>(() => audioSample.ToMono(Side.Left));
        }

        [Test]
        public void ToMono_Exception_OnHighLowChannelsCount()
        {
            var audioSample = new AudioSample(new byte[] {1, 2, 3, 4}, PcmEncoding.PCM8Bit, 4);
            Assert.Throws<InvalidOperationException>(() => audioSample.ToMono(Side.Left));
        }


        [Test]
        public void ToMono_Exception_RawDataMustBeMultipleOf2()
        {
            var audioSample = new AudioSample(new byte[] {1, 2, 3}, PcmEncoding.PCM8Bit, 2);
            Assert.Throws<InvalidOperationException>(() => audioSample.ToMono(Side.Left));
        }

        [Test]
        public void ToMono_Exception_RawDataMustBeAtLeast2()
        {
            var audioSample = new AudioSample(new byte[] {1,}, PcmEncoding.PCM8Bit, 2);
            Assert.Throws<InvalidOperationException>(() => audioSample.ToMono(Side.Left));
        }
    }
}