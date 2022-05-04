using System;

namespace EasyConsoleFramework.IO
{
    public class BaseIO
    {
        public static string ReadFromConsole(string msg, Func<string, bool> condition = null)
        {
            string input;

            if (condition == null)
            {
                Console.Write(msg);
                return Console.ReadLine();
            }

            do
            {
                Console.Write(msg);
                input = Console.ReadLine();
            } while (!condition(input));

            return input;
        }
    }
}