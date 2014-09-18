using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeapYearKata
{
    public static class LeapCalculator
    {
        public static bool IsLeap(int year)
        {
            if (year % 4 != 0)
            {
                return false;
            }
                        
            return (year % 100 != 0 || year % 400 == 0);
        }

        private static bool IsTypicalLeapYear(int year)
        {
            return (year % 4 == 0);
        }

    }
}
