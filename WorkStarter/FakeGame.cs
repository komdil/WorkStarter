using System;
using System.Collections.Generic;
using System.Threading;

namespace WorkStarter
{
    public static class FakeGame
    {
        // public static int FakeBalance { get; set; } = 100;

        public static int GetFakeNumber(List<int> blackNumbers, List<int> redNumbers)
        {
            Thread.Sleep(100);
            var isRed = new Random().Next(0, 10) > 4;
            if (isRed)
            {
                var index = new Random().Next(0, redNumbers.Count);
                return redNumbers[index];
            }
            else
            {
                var index = new Random().Next(0, blackNumbers.Count);
                return blackNumbers[index];
            }
        }
    }
}
