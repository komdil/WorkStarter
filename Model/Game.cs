using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Point = System.Drawing.Point;

namespace Model
{
    public class Logger
    {
        public static void Write(string text)
        {
            using (StreamWriter wr = new StreamWriter("Logs.txt", true))
            {
                wr.WriteLine(text);
            }
        }
    }

    public class Game
    {
        public Game(List<int> blackNumbers, List<int> redNumbers, Point rullateResultNumberXCoor, Point rullateResultNumberYCoor, double balance, bool isFakeGame = false)
        {
            BlackNumbers = blackNumbers;
            RedNumbers = redNumbers;
            RullateResultNumberXCoor = rullateResultNumberXCoor;
            RullateResultNumberYCoor = rullateResultNumberYCoor;
            Balance = balance;
            IsFakeGame = isFakeGame;
        }

        #region Properties of game

        public double Balance { get; set; }
        public int RedXPosition { get; set; }
        public int RedYPosition { get; set; }
        public int BlackXPosition { get; set; }
        public int BlackYPosition { get; set; }
        public List<int> BlackNumbers { get; set; }
        public List<int> RedNumbers { get; set; }
        public Point RullateResultNumberXCoor { get; set; }
        public Point RullateResultNumberYCoor { get; set; }
        public bool IsFakeGame { get; set; }
        public Task ReadNumberOfRullateResult { get; set; }

        #endregion

        #region Read Rullate number Tasks



        bool isRealNumberGetterWorking;

        void ReadRullateNumber()
        {
            int numberOfClick = 0;
            while (isRealNumberGetterWorking)
            {
                Thread.Sleep(100);
                var isRullateStopped = TryGetRullateNumber(out int number);
                if (isRullateStopped)
                {
                    if (RedNumbers.Contains(number))
                    {
                        Logger.Write("Сурх омад");
                        if (MyStavka == Color.Red)
                        {
                            Logger.Write("Ставкаи мухон Сурх буд");
                            Balance += 10 * numberOfClick;
                            Logger.Write($"Буридим баланс {Balance}");
                            numberOfClick = 1;
                            Stavka(Color.Red, numberOfClick);
                            // I won
                        }
                        else if (MyStavka == Color.Black)
                        {
                            Logger.Write("Ставкаи мухон Сиёх буд");
                            Balance -= numberOfClick * 10;
                            Logger.Write($"Бойдодим баланс {Balance}");
                            numberOfClick = numberOfClick * 2;
                            Stavka(Color.Red, numberOfClick);
                            // I lose
                        }
                        else
                        {
                            Logger.Write("Ставкая а сурх сар кун");
                            Stavka(Color.Red, 1);
                            numberOfClick = 1;
                        }
                    }
                    else if (BlackNumbers.Contains(number))
                    {
                        Logger.Write("Сиёх омад");
                        if (MyStavka == Color.Black)
                        {
                            Logger.Write("Ставка сиёх буд");
                            Balance += 10 * numberOfClick;
                            Logger.Write($"Буридим баланс {Balance}");
                            numberOfClick = 1;
                            Stavka(Color.Black, numberOfClick);
                            // I won
                        }
                        else if (MyStavka == Color.Red)
                        {
                            Logger.Write("Ставка сурх буд");
                            Balance -= numberOfClick * 10;
                            Logger.Write($"Бойдодим баланс {Balance}");
                            numberOfClick = numberOfClick * 2;
                            Stavka(Color.Black, numberOfClick);
                            // I lose
                        }
                        else
                        {
                            Stavka(Color.Black, 1);
                            numberOfClick = 1;
                        }
                    }
                    else
                    {
                        //0
                    }
                }
            }
        }

        bool TryGetRullateNumber(out int number)
        {
            var imageOfNumber = CaptureMaker.CaptureActiveWindow(RullateResultNumberXCoor, RullateResultNumberYCoor);
            var textFromImageOfNumber = Scanner.ReadTextUsingTesseract(imageOfNumber);
            var formattedTextFromImageOfNumber = Helper.AutoCorrectNumberFromText(textFromImageOfNumber);

            #region FakeGame                                

            if (IsFakeGame)
            {
                number = FakeGame.GetFakeNumber(BlackNumbers, RedNumbers);
                return true;
            }

            #endregion

            try
            {
                number = int.Parse(formattedTextFromImageOfNumber);
                return true;
            }
            catch
            {
                number = 0;
                return false;
            }

        }

        #endregion

        public Color? MyStavka { get; set; } = null;

        void Stavka(Color redOrBalck, int clickCount)
        {
            MyStavka = redOrBalck;
            var stavkaColor = redOrBalck == Color.Red ? "Сурх" : "Сиёх";
            Logger.Write($"Ставка: {clickCount} бор {stavkaColor}а зер кун");
            if (redOrBalck == Color.Red)
            {
                for (int i = 0; i < clickCount; i++)
                {
                    //MouseClicker.LeftMouseClick(RedXPosition, RedYPosition);
                }
            }
            else
            {
                for (int i = 0; i < clickCount; i++)
                {
                    //MouseClicker.LeftMouseClick(BlackXPosition, BlackYPosition);
                }
            }
        }

        public void Start()
        {
            ReadNumberOfRullateResult = new Task(new Action(ReadRullateNumber));
            Thread.Sleep(2000);
            isRealNumberGetterWorking = true;
            ReadNumberOfRullateResult.Start();
        }

        public void Stop()
        {
            isRealNumberGetterWorking = false;
            MyStavka = null;
        }
    }

    public enum Color
    {
        Red,
        Black
    }
}
