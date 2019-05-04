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
        [TestCase(1,1)]
        [TestCase(2,0)]
        [TestCase(2,1)]
        [TestCase(5,1)]
        [TestCase(5,4)]
        public void RawSound_SubscribeAndUnsubscribe_WorksAsExpected(int subscribeCount, int unsubscribeCount)
        {
            // ARRANGE
            Assert.GreaterOrEqual(subscribeCount, unsubscribeCount, "Subscribe count must be greater or equal than unsubscribe count");

            var expectedCallbacksCount = subscribeCount - unsubscribeCount;
            var countdownCallbackEvent = new CountdownEvent(subscribeCount);
            EventHandler<EventArgs> onPlaybackFinished = (sender, args) =>
            {
                Assert.IsFalse(countdownCallbackEvent.IsSet, "Count of callbacks exceeded");
                countdownCallbackEvent.Signal();
            };

            // ACT (Subscribe, Unsubscribe, Play and Wait)
            for (var i = 0; i < subscribeCount; i++) _sut.PlaybackFinished += onPlaybackFinished;
            for (var i = 0; i < unsubscribeCount; i++) _sut.PlaybackFinished -= onPlaybackFinished;
            _sut.Play();
            countdownCallbackEvent.Wait(50);
            
            // ASSERT (Verify count of callbacks)
            var actualCallbacksCount = countdownCallbackEvent.InitialCount - countdownCallbackEvent.CurrentCount;
            Assert.AreEqual(expectedCallbacksCount, actualCallbacksCount);
        }
    }
}