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
        public void RawSound_OnSubscribe_IsCallback()
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
        [TestCase(0,1,1)]
        [TestCase(1,3,2)]
        [TestCase(3,5,2)]
        public void RawSound_UnsubscribeFromPlaybackFished_WorksAsExpected(int expectedCount, int subscribeCount, int unsubscribeCount)
        {
            // ARRANGE
            int callbacksCount = 0;
            EventHandler<EventArgs> onPlaybackFinished = (sender, args) => { callbacksCount++; };

            // ACT
            for (var i = 0; i < subscribeCount; i++) _sut.PlaybackFinished += onPlaybackFinished;
            for (var i = 0; i < unsubscribeCount; i++) _sut.PlaybackFinished -= onPlaybackFinished;
            _sut.Play();
            Thread.Sleep(100);
            
            // ASSERT
            Assert.AreEqual(expectedCount, callbacksCount);
        }
    }
}