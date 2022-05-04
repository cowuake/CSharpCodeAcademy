using System;
using System.Collections.Generic;
using System.Text;

namespace EasyConsoleFramework.Utils
{
    public static class Converters
    {
        public static bool CanBeBool(this string text, out bool? value)
        {
            string lowered = text.Trim().ToLower();
            
            if (new List<string> { "y", "yes", "t", "true" }.Contains(lowered))
            {
                value = true;
                return true;
            }

            if (new List<string> { "n", "no", "f", "false" }.Contains(lowered))
            {
                value = false;
                return true;
            }

            value = null;
            return false;
        }
    }
}