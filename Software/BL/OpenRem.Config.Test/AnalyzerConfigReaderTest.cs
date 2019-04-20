using Microsoft.Extensions.Configuration;
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
            var configuration = new Mock<IConfiguration>();
            
            var sut = new AnalyzerConfigReader(configuration.Object);
            
        }
    }
}
