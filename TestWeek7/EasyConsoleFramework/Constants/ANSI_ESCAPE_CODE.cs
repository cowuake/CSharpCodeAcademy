using System;
using System.Collections.Generic;
using System.Text;

namespace EasyConsoleFramework.Constants
{
    internal static class ANSI_ESCAPE_CODE
    {
        public const string RESET = "\x1B[0m";
        public const string BOLD = "\x1B[1m";
        public const string DIM = "\x1B[2m";
        public const string ITALIC = "\x1B[3m";
        public const string UNDERLINED = "\x1B[4m";
        public const string NOT_BOLD = "\x1B[22m";
        public const string NOT_ITALIC = "\x1B[23m";
        public const string NOT_UNDERLINED = "\x1B[24m";
    }
}