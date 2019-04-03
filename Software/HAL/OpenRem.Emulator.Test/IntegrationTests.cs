using System.Linq;
using NUnit.Framework;
using OpenRem.Common;

namespace OpenRem.Emulator.Test
{
    [TestFixture]
    public class IntegrationTests
    {
        [Test]
        public void ShouldHaveAtLeastOneRawFile()
        {
            var embeddedSample = new EmbeddedSample();
            Assert.GreaterOrEqual(embeddedSample.GetSamples().Count(), 1);
        }
    }
}
