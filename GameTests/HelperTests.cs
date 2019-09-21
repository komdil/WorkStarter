using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;

namespace GameTests
{
    [TestClass]
    public class HelperTests
    {
        [TestMethod]
        public void HasSpancesToEnd()
        {
            var numberWithWhiteSpances = "17          \n";
            var seventeen = Helper.AutoCorrectNumberFromText(numberWithWhiteSpances);
            Assert.AreEqual("17", seventeen);
        }

        [TestMethod]
        public void HasSpancesToBegining()
        {
            var numberWithWhiteSpances = " \n   17          \n";
            var seventeen = Helper.AutoCorrectNumberFromText(numberWithWhiteSpances);
            Assert.AreEqual("17", seventeen);
        }

        [TestMethod]
        public void GetChancesTest()
        {
            var chances = Helper.GetChances(100, 10);
            Assert.AreEqual(3, chances);
        }
    }
}
