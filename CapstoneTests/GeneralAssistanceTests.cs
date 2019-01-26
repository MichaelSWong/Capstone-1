using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Class;

namespace CapstoneTests
{
    [TestClass]
    public class GeneralAssistanceTests
    {
        [TestMethod]
        public void CheckIntNumberTests()
        {
            Assert.AreEqual(true, GeneralAssistance.CheckIfIntNumber('5'), "5");
            Assert.AreEqual(false, GeneralAssistance.CheckIfIntNumber('-'), "-");

            Assert.AreEqual(true, GeneralAssistance.CheckIfIntNumber("1"), "1");
            Assert.AreEqual(true, GeneralAssistance.CheckIfIntNumber("45"), "45");
            Assert.AreEqual(true, GeneralAssistance.CheckIfIntNumber("6783"), "6783");
            Assert.AreEqual(true, GeneralAssistance.CheckIfIntNumber("-67"), "-67");

            Assert.AreEqual(false, GeneralAssistance.CheckIfIntNumber("4g"), "4g");
            Assert.AreEqual(false, GeneralAssistance.CheckIfIntNumber("valid"), "valid");
            Assert.AreEqual(false, GeneralAssistance.CheckIfIntNumber("g3h7j4"), "g3h7j4");
            Assert.AreEqual(false, GeneralAssistance.CheckIfIntNumber("h-7"), "h-7");
            Assert.AreEqual(false, GeneralAssistance.CheckIfIntNumber("2.5"), "2.5");
            Assert.AreEqual(false, GeneralAssistance.CheckIfIntNumber("-"), "-");
        }
        [TestMethod]
        public void CheckIntNumberInvalidTests()
        {
            Assert.AreEqual(false, GeneralAssistance.CheckIfIntNumber(null), "Null is not a number.");
            Assert.AreEqual(false, GeneralAssistance.CheckIfIntNumber(""), "Empty string is not a number.");
        }

        [TestMethod]
        public void CheckFloatingPointNumberTests()
        {
            Assert.AreEqual(true, GeneralAssistance.CheckIfFloatingPointNumber("1"), "1");
            Assert.AreEqual(true, GeneralAssistance.CheckIfFloatingPointNumber("45"), "45");
            Assert.AreEqual(true, GeneralAssistance.CheckIfFloatingPointNumber("6783"), "6783");
            Assert.AreEqual(true, GeneralAssistance.CheckIfFloatingPointNumber("-67"), "-67");
            Assert.AreEqual(true, GeneralAssistance.CheckIfFloatingPointNumber("2.5"), "2.5");
            Assert.AreEqual(true, GeneralAssistance.CheckIfFloatingPointNumber(".25"), ".25");
            Assert.AreEqual(true, GeneralAssistance.CheckIfFloatingPointNumber("2.5567"), "2.5567");

            Assert.AreEqual(false, GeneralAssistance.CheckIfFloatingPointNumber("4g"), "4g");
            Assert.AreEqual(false, GeneralAssistance.CheckIfFloatingPointNumber("valid"), "valid");
            Assert.AreEqual(false, GeneralAssistance.CheckIfFloatingPointNumber("g3h7j4"), "g3h7j4");
            Assert.AreEqual(false, GeneralAssistance.CheckIfFloatingPointNumber("h-7"), "h-7");
            Assert.AreEqual(false, GeneralAssistance.CheckIfFloatingPointNumber("2.5.6"), "2.5.6");
            Assert.AreEqual(false, GeneralAssistance.CheckIfIntNumber("-"), "-");
            Assert.AreEqual(false, GeneralAssistance.CheckIfIntNumber("."), ".");
        }
        [TestMethod]
        public void CheckFloatingPointNumberInvalidTests()
        {
            Assert.AreEqual(false, GeneralAssistance.CheckIfFloatingPointNumber(null), "Null is not a number.");
            Assert.AreEqual(false, GeneralAssistance.CheckIfFloatingPointNumber(""), "Empty string is not a number.");
        }
    }
}
