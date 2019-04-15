using System.Linq;
using NUnit.Framework;

namespace OpenRem.Emulator.Test
{
    [TestFixture]
    public class IntegrationTest
    {
        [Test]
        public void ShouldHaveAtLeastOneRawFile()
        {
            var embeddedSample = new EmbeddedSample();
            Assert.GreaterOrEqual(embeddedSample.GetSamples().Count(), 1);
        }
    }
}
