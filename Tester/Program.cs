using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            var txt = "17          \n";
            var res = string.Join("", txt.ToCharArray().Where(Char.IsDigit));
        }
    }
}
