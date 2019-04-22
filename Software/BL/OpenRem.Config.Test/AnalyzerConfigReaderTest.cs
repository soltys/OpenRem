using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using OpenRem.Common;
using OpenRem.Config.Infrastructure;
using OpenRem.Config.Reader;

namespace OpenRem.Config.Test
{
    [TestFixture]
    public class AnalyzerConfigReaderTest
    {
        [Test]
        [TestCase("MKRZERO")]
        [TestCase("Emulator")]
        public void GetConfig(string configName)
        {
            var crp = new BusinessLogicConfigurationProvider();
            var sut = new AnalyzerCollectionConfigReader(crp.GetConfigurationRoot());

            var config = sut.GetConfig(configName);

            Assert.IsNotNull(config);
            Assert.AreEqual(configName, config.Name);
        }
    }
}