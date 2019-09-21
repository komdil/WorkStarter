using System.Drawing;
using Tesseract;

namespace Model
{
    public class Scanner
    {
        public static string ReadTextUsingTesseract(Bitmap bitmap)
        {
            TesseractEngine engine = new TesseractEngine("./tessdata", "eng");
            var page = engine.Process(bitmap, PageSegMode.Auto);
            return page.GetText();
        }
        /* 
         *  var bitmap = CaptureMaker.CaptureActiveWindow(new System.Drawing.Point(32, 84), new System.Drawing.Point(404, 651));
         *  bitmap.Save("E:/text.png", ImageFormat.Png);
         *  var text = Scanner.GetTextFromImage(bitmap);
         *  MessageBox.Show(text);
         */
    }
}
