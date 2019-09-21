using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace MouseCoordinationGetter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Helpers

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public static implicit operator Point(POINT point)
            {
                return new Point(point.X, point.Y);
            }
        }

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);

        public static Point GetCursorPosition()
        {
            POINT lpPoint;
            GetCursorPos(out lpPoint);
            return lpPoint;
        }


        private GlobalKeyboardHook _globalKeyboardHook;
        #endregion

        bool isStarted;
        public App CurrentApp { get; internal set; }

        Task startGetCoordinateOfMouseTask;
        public MainWindow()
        {
            CurrentApp = Application.Current as App;
            InitializeComponent();
            startGetCoordinateOfMouseTask = new Task(new Action(StartGetCoordinateOfMouse));
            startGetCoordinateOfMouseTask.Start();
            isStarted = true;
            _globalKeyboardHook = new GlobalKeyboardHook();
            _globalKeyboardHook.KeyboardPressed += _globalKeyboardHook_KeyboardPressed;
        }

        private void _globalKeyboardHook_KeyboardPressed(object sender, GlobalKeyboardHookEventArgs e)
        {
            var virtualCode = e.KeyboardData.VirtualCode;
            //27 is virtual code of ESC button
            if (virtualCode == 27)
            {
                isStarted = false;
                StartButton.Background = Brushes.Green;
                StartButton.Content = "Start";
            }
        }

        public void StartGetCoordinateOfMouse()
        {
            while (isStarted)
            {
                var point = GetCursorPosition();
                this.Dispatcher.Invoke(() =>
                {
                    XTextBox.Text = point.X.ToString();
                    YTextBox.Text = point.Y.ToString();
                });
                Thread.Sleep(200);
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            isStarted = true;
            if (startGetCoordinateOfMouseTask.IsCompleted)
            {
                startGetCoordinateOfMouseTask = new Task(new Action(StartGetCoordinateOfMouse));
                startGetCoordinateOfMouseTask.Start();
                StartButton.Background = Brushes.Red;
                StartButton.Content = "Press ESC to stop";
            }
        }

        public void Stop()
        {
            isStarted = false;
        }
    }
}
