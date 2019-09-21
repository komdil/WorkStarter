using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Model
{
    public class FocusSetter
    {
        #region Helpers

        //API-declaration
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        #endregion
        Process Process;
        Task SetFocusAllTimesTask;
        public FocusSetter(Process process, bool setFocusAllTime = false)
        {
            Process = process;
            SetFocusAllTimesTask = new Task(new Action(SetFocusAllTimes));
            if (setFocusAllTime)
            {
                SetFocusAllTimesTask.Start();
            }
        }
        public static Process[] AllProcesses = Process.GetProcesses();
        public void SetFocusAllTimes()
        {
            while (true)
            {
                Thread.Sleep(1500);
                SetFocus();
            }
        }
        public void SetFocus()
        {
            IntPtr ipHwnd = Process.MainWindowHandle;
            SetForegroundWindow(ipHwnd);
        }

        public static Process GetHasWindowProcess(string processName)
        {
            var processes = AllProcesses.Where(a => a.ProcessName == processName);
            foreach (Process process in processes)
            {
                // the chrome process must have a window
                if (process.MainWindowHandle == IntPtr.Zero)
                    continue;
                return process;
            }
            return null;
        }
    }
}
