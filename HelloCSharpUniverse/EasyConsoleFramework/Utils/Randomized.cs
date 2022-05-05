using EasyConsoleFramework.Constants;
using EasyConsoleFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyConsoleFramework.Utils
{
    public static class Randomized
    {
        public static DateTime RandomDateTime(int minYear = 1950)
        {
            Random r = new Random();

            int year = r.Next(minYear, DateTime.Now.Year);
            int month = r.Next(1, 12);
            int day = r.Next(1, DateTime.DaysInMonth(year, month));

            int h = r.Next(0, 24);
            int m = r.Next(0, 60);
            int s = r.Next(0, 60);

            return new DateTime(year, month, day, h, m, s);
        }

        public static string RandomName()
        {
            Random r = new Random();
            int firstNameIdx = r.Next(0, DICTIONARIES.FIRST_NAMES.Count);
            int lastNameIdx = r.Next(0, DICTIONARIES.LAST_NAMES.Count);

            return $"{DICTIONARIES.FIRST_NAMES[firstNameIdx]} {DICTIONARIES.LAST_NAMES[lastNameIdx]}";
        }

        public static int RandomId(int min = 1, int max = int.MaxValue)
            => new Random().Next(min, max);

        public static decimal RandomMoney()
            => RandomId() + RandomId().ToFractional();
    }
}