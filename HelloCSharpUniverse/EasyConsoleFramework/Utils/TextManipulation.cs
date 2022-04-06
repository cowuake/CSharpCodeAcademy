using System;
using System.Collections.Generic;
using System.Text;

namespace EasyConsoleFramework.Utils
{
    public static class TextManipulation
    {
        public static string PadUntilLimit(this string str, int limit)
        {
            if (str == null || limit < str.Length + 2)
                throw new ArgumentException();

            int availableSpace = limit - str.Length;
            int leftPadding = availableSpace / 2;
            int rightPadding = availableSpace - leftPadding;

            return str.PadLeft(leftPadding).PadRight(rightPadding);
        }
    }
}