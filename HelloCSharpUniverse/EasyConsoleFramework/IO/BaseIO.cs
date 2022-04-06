using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyConsoleFramework.ExtensionMethods;

namespace EasyConsoleFramework.IO
{
    public class BaseIO
    {
        public static string ReadFromConsole(string msg, Func<string, bool> condition)
        {
            string input;

            do
            {
                Console.Write(msg);
                input = Console.ReadLine();
            } while (!condition(input));

            return input;
        }
    }
}
