using Moq;
using NUnit.Framework;
using OpenRem.Common;

namespace OpenRem.Config.Test
{
    [TestFixture]
    public class AnalyzerConfigReaderTest
    {
        [Test]
        public void GetConfig_For_MKRZero()
        {
            //var embeddedConfig = new Mock<IEmbeddedConfig>();
            //var sut = new AnalyzerConfigReader(embeddedConfig.Object);
            //embeddedConfig.Setup(x => x.GetConfigFile("AnalyzerConfig.xml"))
            //    .Returns(() => typeof(AnalyzerConfigReaderTest).Assembly.GetResourceStream("SampleArduinoConfig.xml"));

            //var config = sut.GetConfig("MKRZERO");

            //Assert.AreEqual("MKRZERO", config.Name);
            //Assert.AreEqual(44100, config.SampleRate);
            //Assert.AreEqual(32, config.SubChunkSize);
            //Assert.AreEqual(2, config.ChannelsNumber);

            //Assert.AreEqual(1, config.Probes.Length);
            //Assert.AreEqual(Side.Left, config.Probes[0].Side);
            //Assert.AreEqual(0, config.Probes[0].InputChannel);
            //Assert.AreEqual(1, config.Probes[0].OutputChannel);
        }
    }
}
