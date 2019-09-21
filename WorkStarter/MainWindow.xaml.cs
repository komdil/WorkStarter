using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using d = System.Drawing;
namespace WorkStarter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Game Game;
        public List<int> RedNumbers { get; set; } = new List<int>();
        public List<int> BlackNumbers { get; set; } = new List<int>();
        public double Balance { get; set; }
        public d.Point XNumberCoor { get; set; }
        public d.Point YNumberCoor { get; set; }

        public string ActiveProcessName { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var msg = (e.ExceptionObject as Exception).Message;
            Logger.Write("Unhundled exception:\n" + msg);
            MessageBox.Show(msg);
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            if (!Validate(out string error))
            {
                MessageBox.Show(error);
            }
            else
            {
                Game = new Game(BlackNumbers, RedNumbers, XNumberCoor, YNumberCoor, Balance);
                Game.Start();
            }
        }
        bool Validate(out string error)
        {
            error = "";
            if (!RedNumbers.Any())
            {
                error += "RedNumbers are incorrect!\n";
            }
            if (!BlackNumbers.Any())
            {
                error += "BlackNumbers are incorrect!\n";
            }
            if (Balance <= 0)
            {
                error += "Balance is incorrect!\n";
            }
            if (XNumberCoor.X <= 0 || XNumberCoor.Y <= 0)
            {
                error += "XNumberCoor is incorrect!\n";
            }
            if (YNumberCoor.X <= 0 || YNumberCoor.Y <= 0)
            {
                error += "YNumberCoor is incorrect!\n";
            }
            if (ActiveProcessName == "")
            {
                error += "ActiveProcessName is incorrect!\n";
            }
            return error == "";
        }

        private void YCoorOfResultNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                var splitted = YCoorOfResultNumber.Text.Split(',').Select(a => int.Parse(a));
                if (splitted.Count() != 2)
                    throw new Exception("It should be like '100,200'");
                YNumberCoor = new d.Point(splitted.First(), splitted.Last());
            }
            catch (Exception ex)
            {
                MessageBox.Show("YCoorOfResultNumber is incorrect \n" + ex.Message);
            }
        }

        private void XCoorOfResultNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                var splitted = XCoorOfResultNumber.Text.Split(',').Select(a => int.Parse(a));
                if (splitted.Count() != 2)
                    throw new Exception("It should be like '100,200'");
                XNumberCoor = new d.Point(splitted.First(), splitted.Last());
            }
            catch (Exception ex)
            {
                MessageBox.Show("XCoorOfResultNumber is incorrect \n" + ex.Message);
            }
        }
        private void NumbersOfRedsTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                RedNumbers = NumbersOfRedsTextBox.Text.Split(',').Select(a => int.Parse(a)).ToList();
            }
            catch (Exception ex)
            {
                RedNumbers = new List<int>();
                MessageBox.Show("RedNumbers is incorrect \n" + ex.Message);
            }
        }

        private void NumbersOfBlacksTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                BlackNumbers = NumbersOfBlacksTextBox.Text.Split(',').Select(a => int.Parse(a)).ToList();
            }
            catch (Exception ex)
            {
                BlackNumbers = new List<int>();
                MessageBox.Show("BlackNumbers is incorrect \n" + ex.Message);
            }
        }

        private void BalanceTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                Balance = double.Parse(BalanceTextBox.Text);
            }
            catch (Exception ex)
            {
                Balance = 0;
                MessageBox.Show("Balance is incorrect \n" + ex.Message);
            }
        }


        private void ActiveWindowProccessName_LostFocus(object sender, RoutedEventArgs e)
        {
            var hasWindow = Process.GetProcesses().Any(a => a.ProcessName == ActiveWindowProccessName.Text && a.MainWindowHandle != IntPtr.Zero);
            if (hasWindow)
            {
                ActiveProcessName = ActiveWindowProccessName.Text;
            }
            else
            {
                MessageBox.Show("ActiveProcessName is incorrect!");
                ActiveProcessName = "";
            }
        }
    }
}
