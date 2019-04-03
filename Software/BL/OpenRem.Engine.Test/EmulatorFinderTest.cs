using Moq;
using NUnit.Framework;
using OpenRem.Emulator;

namespace OpenRem.Engine.Test
{
    [TestFixture]
    public class EmulatorFinderTest
    {
        private Mock<IEmbeddedSample> embeddedSampleMock;
        private EmulatorFinder sut;

        [SetUp]
        public void Init()
        {
            this.embeddedSampleMock = new Mock<IEmbeddedSample>();
            this.sut = CreateSut();
        }

        [Test]
        public void NoFilesFound_NoEmulators()
        {
            var emulators = this.sut.GetEmulators();
            Assert.IsNotNull(emulators);
            Assert.AreEqual(0, emulators.Length);
        }

        [Test]
        public void OneEmbeddedSample()
        {
            const string sampleName = "emulator.raw";
            this.embeddedSampleMock.Setup(x => x.GetSamples()).Returns(() => new[]
            {
               sampleName
            });

            var emulators = this.sut.GetEmulators();
            
            Assert.AreEqual(1, emulators.Length);
            Assert.AreEqual(sampleName, emulators[0].SignalName);
            Assert.AreEqual(true, emulators[0].EmbeddedSignal);
        }

        private EmulatorFinder CreateSut()
        {
            return new EmulatorFinder(this.embeddedSampleMock.Object);
        }
    }
}
