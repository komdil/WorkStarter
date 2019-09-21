using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Model
{
    public class CaptureMaker
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        static extern IntPtr GetDesktopWindow();

        [StructLayout(LayoutKind.Sequential)]
        private struct Rect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
        // var bitmap = CaptureMaker.CaptureActiveWindow(new System.Drawing.Point(32, 84), new System.Drawing.Point(404, 651));

        //public static Image CaptureDesktop()
        //{
        //    return CaptureWindow(GetDesktopWindow());
        //}

        public static Bitmap CaptureActiveWindow(Point firstPoint, Point secondPoint)
        {
            return CaptureWindow(GetForegroundWindow(), firstPoint, secondPoint);
        }

        static Bitmap CaptureWindow(IntPtr handle, Point firstPoint, Point secondPoint)
        {
            var bounds = new Rectangle(firstPoint.X, firstPoint.Y, secondPoint.X - firstPoint.X, secondPoint.Y - firstPoint.Y);
            var result = new Bitmap(secondPoint.X - firstPoint.X, secondPoint.Y - firstPoint.Y);

            using (var graphics = Graphics.FromImage(result))
            {
                graphics.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
            }
            return result;
        }

        /*  USAGE:
         *  var image = CaptureMaker.CaptureActiveWindow(new System.Drawing.Point(31, 84), new System.Drawing.Point(407, 568));
         *  image.Save(@"E:\snippetsource.jpg", ImageFormat.Png);
         */
    }
}
