using System.Linq;
using NUnit.Framework;

namespace OpenRem.Config.Test
{
    [TestFixture]
    public class IntegrationTest
    {
        [Test]
        public void AtLeastOneConfigFile()
        {
            EmbeddedConfig embeddedConfig = new EmbeddedConfig();
            var xmlConfigFiles = embeddedConfig.GetFiles(".xml").ToArray();

            Assert.GreaterOrEqual(1, xmlConfigFiles.Length);
        }
    }
}
