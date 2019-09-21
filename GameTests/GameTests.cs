using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System.Collections.Generic;
using System.Drawing;

namespace GameTests
{
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        public void GameStartTest()
        {
            var blackNumbers = new List<int>()
                {
                    2, 4, 6, 8, 10, 11, 13, 15, 17, 20, 22, 24, 26, 28, 29, 31, 33, 35
                };
            var redNumbers = new List<int>()
                {
                    1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36
                };
            var Game = new Game(blackNumbers, redNumbers, new Point(662, 665), new Point(697, 699), 1000, true);
            Game.Start();
            Game.ReadNumberOfRullateResult.Wait();
        }
    }
}
