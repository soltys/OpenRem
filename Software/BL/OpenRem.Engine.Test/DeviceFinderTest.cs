using System.Linq;
using Moq;
using NUnit.Framework;
using OpenRem.Engine.OS;

namespace OpenRem.Engine.Test
{
    [TestFixture]
    public class DeviceFinderTest
    {
        private DeviceFinder sut;
        private Mock<IPnPDevice> pnpDeviceMock;

        [SetUp]
        public void Init()
        {
            this.pnpDeviceMock = new Mock<IPnPDevice>();
            this.sut = CreateSut();
        }

        [Test]
        public void GetPossibleArduinoDevices_SingleArduino()
        {
            this.pnpDeviceMock.Setup(x => x.GetDevices()).Returns(new[]
            {
                "Arduino Leonardo (COM10)"
            });
            var possibleArduinoDevice = sut.GetArduinoDevices().ToArray();

            Assert.AreEqual(1,possibleArduinoDevice.Length);
            Assert.AreEqual("Arduino Leonardo", possibleArduinoDevice[0].Name);
            Assert.AreEqual("COM10", possibleArduinoDevice[0].ComPort);
        }

        [Test]
        public void GetPossibleArduinoDevices_MultipleArduino()
        {
            this.pnpDeviceMock.Setup(x => x.GetDevices()).Returns(new[]
            {
                "Arduino Leonardo (COM10)",
                "Arduino MKRZERO (COM11)"
            });
            var possibleArduinoDevice = sut.GetArduinoDevices().ToArray();

            Assert.AreEqual(2, possibleArduinoDevice.Length);
            Assert.AreEqual("Arduino Leonardo", possibleArduinoDevice[0].Name);
            Assert.AreEqual("COM10", possibleArduinoDevice[0].ComPort);

            Assert.AreEqual("Arduino MKRZERO", possibleArduinoDevice[1].Name);
            Assert.AreEqual("COM11", possibleArduinoDevice[1].ComPort);
        }

        [Test]
        public void GetPossibleArduinoDevices_IgnoringNonArduinoDevices()
        {
            this.pnpDeviceMock.Setup(x => x.GetDevices()).Returns(new[]
            {
                "Arduino Leonardo (COM10)",
                "Communication Port (COM1)"
            });
            var possibleArduinoDevice = sut.GetArduinoDevices().ToArray();

            Assert.AreEqual(1, possibleArduinoDevice.Length);
            Assert.AreEqual("Arduino Leonardo", possibleArduinoDevice[0].Name);
            Assert.AreEqual("COM10", possibleArduinoDevice[0].ComPort);
        }

        [Test]
        public void GetPossibleArduinoDevices_NoCompatibleDevices_ReturnEmptyArray()
        {
            this.pnpDeviceMock.Setup(x => x.GetDevices()).Returns(new[]
            {
                "Communication Port (COM1)"
            });
            var possibleArduinoDevice = sut.GetArduinoDevices();

            Assert.IsNotNull(possibleArduinoDevice);
        }

        private DeviceFinder CreateSut()
        {
            return new DeviceFinder(pnpDeviceMock.Object);
        }
    }
}
