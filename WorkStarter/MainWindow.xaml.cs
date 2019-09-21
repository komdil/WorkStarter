using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
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
        public MainWindow()
        {
            //Image image = Image.FromFile("D:\\9.png");
            //Bitmap bitmap = new Bitmap(image);
            //var text = Scanner.ReadTextUsingTesseract(bitmap);
            InitializeComponent();
            //this.Hide();
            // var chromeProcess = FocusSetter.GetHasWindowProcess("chrome");
            //FocusSetter focusSetter = new FocusSetter(chromeProcess, true);
            // Process.GetCurrentProcess().Kill();
            var blackNumbers = new List<int>()
                {
                    2, 4, 6, 8, 10, 11, 13, 15, 17, 20, 22, 24, 26, 28, 29, 31, 33, 35
                };
            var redNumbers = new List<int>()
                {
                    1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36
                };
            Game = new Game(blackNumbers, redNumbers, new d.Point(662, 665), new d.Point(697, 699), 1000);
            Game.LoggerListBox = Logger;
            //Process.GetCurrentProcess().Kill();
        }

        string GetChances()
        {
            try
            {
                long stavka = 10;
                long balance = long.Parse(Balance.Text);
                int count = 0;
                while (balance > stavka)
                {
                    count++;
                    balance -= stavka;
                    stavka = stavka * 2;
                }
                return count.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private static Image GetImage()
        {
            return Image.FromFile("D:\\snippetsource.png");
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            Game.Stop();
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            Game.Start();
        }

        private void Balance_FocusableChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
          
        }

        private void Balance_LostFocus(object sender, RoutedEventArgs e)
        {
            Chances.Text = GetChances();
        }
    }
}
