using NUnit.Framework;
using OpenRem.Arduino;

namespace OpenRem.Engine.Test
{
    [TestFixture]
    class ArduinoNameParserTest
    {
        [TestCase("Arduino Leonardo", ArduinoType.Leonardo)]
        [TestCase("Arduino MKRZERO", ArduinoType.MKRZERO)]
        public void ArduinoNameParser_TestCases(string input, ArduinoType expectedOutput)
        {
            Assert.AreEqual(expectedOutput, ArduinoNameParser.ToArduinoType(input));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("asdasd")]
        [TestCase("   ")]
        public void ArduinoNameParser_IfInvalidData_DefaultToMKRZero(string input)
        {
            Assert.AreEqual(ArduinoType.MKRZERO, ArduinoNameParser.ToArduinoType(input));
        }
    }
}
