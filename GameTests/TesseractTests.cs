using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System.Drawing;

namespace GameTests
{
    [TestClass]
    public class TesseractTests
    {
        [TestMethod]
        public void ReadTextNumberFromBitmap()
        {
            var bitmap = new Bitmap("TestData\\seventeenimage.PNG");
            var text = Scanner.ReadTextUsingTesseract(bitmap);
            var seventeen = Helper.AutoCorrectNumberFromText(text);
            Assert.AreEqual("17", seventeen);
        }
    }
}
