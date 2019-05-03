using System;
using System.IO;
using System.Threading;
using NAudio.Wave;
using NUnit.Framework;

namespace AudioTools.UnitTests
{
    [TestFixture]
    public class Tests
    {
        private RawSound _sut;

        private RawSound CreateSystemUnderTest()
        {
            var rawWaveStream = new RawSourceWaveStream(new MemoryStream(new byte[] { }), new WaveFormat(100, 16, 1));
            var waveOutEvent = new WaveOutEvent();
            waveOutEvent.Init(rawWaveStream);
            var sound = new RawSound(waveOutEvent);
            return sound;
        }

        [SetUp]
        public void SetUp()
        {
            _sut = CreateSystemUnderTest();
        }
    
        [Test]
        public void RawSound_PlaybackFinishedSubscription_IsHandled()
        {
            // ARRANGE
            bool isCallback = false;
            _sut.PlaybackFinished += (sender, args) => { isCallback = true; };

            // ACT
            _sut.Play();
            Thread.Sleep(100);

            // ASSERT
            Assert.IsTrue(isCallback);
        }

        [Test]
        public void RawSound_OnUnsubscribe_NoCallback()
        {
            // ARRANGE
            
            
            // ACT
            
            
            // ASSERT
            throw new NotImplementedException();
        }
    }
}