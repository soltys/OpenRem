using System;
using System.IO;
using System.Threading;
using AudioTools.Interface;
using NAudio.Wave;
using NUnit.Framework;

namespace AudioTools.UnitTests
{
    [TestFixture]
    public class Tests
    {
        /// <summary>
        /// Wraps given type into RawSound
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private RawSound CreateSystemUnderTest<T>() where T : IWavePlayer, new()
        {
            var rawWaveStream = new RawSourceWaveStream(new MemoryStream(new byte[] { }), new WaveFormat(100, 16, 1));
            var wavePlayer = new T();
            wavePlayer.Init(rawWaveStream);
            var sound = new RawSound(wavePlayer);
            return sound;
        }

        [Test]
        [TestCase(1,1)]
        [TestCase(2,0)]
        [TestCase(2,1)]
        [TestCase(5,1)]
        [TestCase(5,4)]
        public void RawSound_WaveOutEvent_SubscribeAndUnsubscribe_WorksAsExpected(int subscribeCount, int unsubscribeCount)
        {
            // ARRANGE
            var sut = CreateSystemUnderTest<WaveOutEvent>();
            TestSubscriptions(sut, subscribeCount, unsubscribeCount);
        }

        [Test]
        [TestCase(1,1)]
        [TestCase(2,0)]
        [TestCase(2,1)]
        [TestCase(5,1)]
        [TestCase(5,4)]
        public void RawSound_WasapiOut_SubscribeAndUnsubscribe_WorksAsExpected(int subscribeCount, int unsubscribeCount)
        {
            // ARRANGE
            var sut = CreateSystemUnderTest<WasapiOut>();
            TestSubscriptions(sut, subscribeCount, unsubscribeCount, 300);
        }
        
        private void TestSubscriptions(ISound sut, int subscribeCount, int unsubscribeCount, int millisecondsTimeout = 50)
        {
            Assert.GreaterOrEqual(subscribeCount, unsubscribeCount,
                "Subscribe count must be greater or equal than unsubscribe count");

            var expectedCallbacksCount = subscribeCount - unsubscribeCount;
            var countdownCallbackEvent = new CountdownEvent(subscribeCount);
            EventHandler<EventArgs> onPlaybackFinished = (sender, args) =>
            {
                Assert.IsFalse(countdownCallbackEvent.IsSet, "Count of callbacks exceeded");
                countdownCallbackEvent.Signal();
            };

            // ACT (Subscribe, Unsubscribe, Play and Wait)
            for (var i = 0; i < subscribeCount; i++) sut.PlaybackFinished += onPlaybackFinished;
            for (var i = 0; i < unsubscribeCount; i++) sut.PlaybackFinished -= onPlaybackFinished;
            sut.Play();
            countdownCallbackEvent.Wait(millisecondsTimeout);

            // ASSERT (Verify count of callbacks)
            var actualCallbacksCount = countdownCallbackEvent.InitialCount - countdownCallbackEvent.CurrentCount;
            Assert.AreEqual(expectedCallbacksCount, actualCallbacksCount);
        }
    }
}