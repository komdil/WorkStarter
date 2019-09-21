using IronOcr;
using System.Drawing;
using IronOcr.Languages;
using Tesseract;

namespace Model
{
    public class Scanner
    {
        public static AutoOcr CreateOCR()
        {
            AutoOcr ocr = new AutoOcr() { ReadBarCodes = false };
            ocr.Language = MultiLanguage.OcrLanguagePack(English.OcrLanguagePack);
            return ocr;
        }

        public static string ReadTextUsingTesseract(Bitmap bitmap)
        {
            TesseractEngine engine = new TesseractEngine("./tessdata", "eng");
            var page = engine.Process(bitmap, PageSegMode.Auto);
            return page.GetText();
        }
        public static string GetTextFromImage(Image image)
        {
            var ocr = CreateOCR();
            return ocr.Read(image).Text;
        }
        public static string GetTextFromImage(Bitmap bitmap)
        {
            var ocr = CreateOCR();
            return ocr.Read(bitmap).Text;
        }

        /* 
         *  var bitmap = CaptureMaker.CaptureActiveWindow(new System.Drawing.Point(32, 84), new System.Drawing.Point(404, 651));
         *  bitmap.Save("E:/text.png", ImageFormat.Png);
         *  var text = Scanner.GetTextFromImage(bitmap);
         *  MessageBox.Show(text);
         */
    }
}
