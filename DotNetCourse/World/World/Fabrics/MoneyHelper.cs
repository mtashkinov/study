using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace World.Fabrics
{
    internal sealed class MoneyHelper
    {
        internal static int MarkToMoney(double avgMark)
        {
            return (int)Math.Pow(10.0, avgMark);
        }

        internal static double MoneyToMark(int money)
        {
            return Math.Log10(money);
        }
    }
}
