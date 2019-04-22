using System;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

namespace OpenRem.Engine.Test
{
    [TestFixture]
    public class DetectManagerTest
    {
        private Mock<IAnalyzerCollection> analyzerCollection;
        private Mock<IDeviceFinder> deviceFinder;
        private Mock<IEmulatorFinder> emulatorFinder;
        private DetectManager sut;

        [SetUp]
        public void Setup()
        {
            this.analyzerCollection = new Mock<IAnalyzerCollection>();
            this.deviceFinder = new Mock<IDeviceFinder>();
            this.emulatorFinder = new Mock<IEmulatorFinder>();
            this.sut = CreateSut();
        }

        private DetectManager CreateSut()
        {
            return new DetectManager(
                this.analyzerCollection.Object,
                this.deviceFinder.Object,
                this.emulatorFinder.Object
            );
        }

        [Test]
        public async Task GetAnalyzers_NothingToBeFound_EmptyCollection()
        {
            var analyzers = await this.sut.GetAnalyzersAsync();
            Assert.IsNotNull(analyzers);
            Assert.AreEqual(0, analyzers.Length);
            this.analyzerCollection.Verify(x => x.Add(It.IsAny<ArduinoDevice>()), Times.Never);
            this.analyzerCollection.Verify(x => x.Add(It.IsAny<Emulator>()), Times.Never);
        }

        [Test]
        public async Task GetAnalyzers_FindArduino()
        {
            var arduinoDevice = new ArduinoDevice
            {
                ComPort = "COM1",
                Name = "Arduino Test"
            };

            this.deviceFinder.Setup(x => x.GetArduinoDevices()).Returns(() => new[]
            {
                arduinoDevice
            });
            var arduinoGuid = Guid.NewGuid();
            this.analyzerCollection.Setup(x => x.Add(It.IsIn(arduinoDevice))).Returns(arduinoGuid);

            var analyzers = await this.sut.GetAnalyzersAsync();

            Assert.IsNotNull(analyzers);
            Assert.AreEqual(1, analyzers.Length);
            Assert.AreEqual("Arduino Test", analyzers[0].Name);
            Assert.AreEqual(arduinoGuid, analyzers[0].Id);

            this.analyzerCollection.Verify(x => x.Add(It.IsAny<ArduinoDevice>()), Times.Once);
            this.analyzerCollection.Verify(x => x.Add(It.IsAny<Emulator>()), Times.Never);
        }


        [Test]
        public async Task GetAnalyzers_FindEmulator()
        {
            var emulator = new Emulator()
            {
                SignalName = "test.raw"
            };

            this.emulatorFinder.Setup(x => x.GetEmulators()).Returns(() => new[]
            {
                emulator
            });
            var emulatorGuid = Guid.NewGuid();
            this.analyzerCollection.Setup(x => x.Add(It.IsIn(emulator))).Returns(emulatorGuid);

            var analyzers = await this.sut.GetAnalyzersAsync();

            Assert.IsNotNull(analyzers);
            Assert.AreEqual(1, analyzers.Length);
            Assert.AreEqual("Emulator - test.raw", analyzers[0].Name);
            Assert.AreEqual(emulatorGuid, analyzers[0].Id);

            this.analyzerCollection.Verify(x => x.Add(It.IsAny<ArduinoDevice>()), Times.Never);
            this.analyzerCollection.Verify(x => x.Add(It.IsAny<Emulator>()), Times.Once);
        }
    }
}