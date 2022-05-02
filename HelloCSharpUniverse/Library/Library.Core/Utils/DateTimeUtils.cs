using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.Utils
{
    public static class DateTimeUtils
    {
        public static DateTime RandomDateTime(int minYear = 1950)
        {
            Random r = new Random();

            int year = r.Next(minYear, DateTime.Now.Year);
            int month = r.Next(1, 12);
            int day = r.Next(1, DateTime.DaysInMonth(year, month));

            int h = r.Next(0, 23);
            int m = r.Next(0, 59);
            int s = r.Next(0, 59);

            return new DateTime(year, month, day, h, m, s);
        }
    }
}