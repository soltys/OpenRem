using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace OpenRem.Common.Test
{
    [TestFixture]
    public class IntegrationTest
    {
        [Test]
        public void Hardcoded_AssemblyNames_AreCorrect()
        {
            Assert.AreEqual("OpenRem.Engine", AppDomainHelper.EngineAssemblyName);
            Assert.AreEqual("OpenRem.Engine.Interface", AppDomainHelper.EngineInterfaceAssemblyName);
        }
    }
}