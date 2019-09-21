using System.Runtime.InteropServices;
using System.Windows.Input;

namespace Model
{
    public class KeyPresser
    {
        #region Helpers

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

        public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        public const int MOUSEEVENTF_LEFTUP = 0x04;

        const uint KEYEVENTF_EXTENDEDKEY = 0x0001;

        #endregion

        public static void Press(string str)
        {

        }

        public static void Press(Key key)
        {

        }
    }
}
