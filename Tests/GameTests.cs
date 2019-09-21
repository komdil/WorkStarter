using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class GettingNumberFromText
    {
        [TestMethod]
        public void HasSpancesToEnd()
        {
            var numberWithWhiteSpances = "17          \n";
            var seventeen = string.Join("", numberWithWhiteSpances.ToCharArray().Where(Char.IsDigit));
            Assert.AreEqual("17", seventeen);
        }

        [TestMethod]
        public void HasSpancesToBegining()
        {
            var numberWithWhiteSpances = " \n   17          \n";
            var seventeen = string.Join("", numberWithWhiteSpances.ToCharArray().Where(Char.IsDigit));
            Assert.AreEqual("17", seventeen);
        }
    }
}
