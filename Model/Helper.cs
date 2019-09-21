using System;
using System.Linq;

namespace Model
{
    public static class Helper
    {
        public static string AutoCorrectNumberFromText(string str)
        {
            return string.Join("", str.ToCharArray().Where(char.IsDigit));
        }

        public static int GetChances(long balance, long stavka)
        {
            int count = 0;
            while (balance > stavka)
            {
                count++;
                balance -= stavka;
                stavka = stavka * 2;
            }
            return count;
        }
    }
}
