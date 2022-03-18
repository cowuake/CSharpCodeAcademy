using System;
using System.Collections.Generic;
using System.Text;

namespace BankAccount.Services
{
    public static class SimpleIO
    {
        public static string ReadFromConsoleConditionally(string msg, Func<string, bool> condition)
        {
            string input;

            do
            {
                Console.Write(msg);
                input = Console.ReadLine().Trim();
            } while (!condition(input));

            return input;
        }

        public static uint ReadUIntFromConsole(string msg)
        {
            uint n;
            string input;

            do
            {
                Console.Write(msg);
                input = Console.ReadLine().Trim();
            } while (!uint.TryParse(input, out n));

            return n;
        }

        public static ushort ReadUShortFromConsole(string msg)
        {
            ushort n;
            string input;

            do
            {
                Console.Write(msg);
                input = Console.ReadLine().Trim();
            } while (!ushort.TryParse(input, out n));

            return n;
        }
    }
}
