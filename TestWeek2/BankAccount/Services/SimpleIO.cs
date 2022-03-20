using System;
using System.Collections.Generic;
using System.Text;

namespace BankAccount.Services
{
    public static class SimpleIO
    {
        public static string ReadFromConsole(string msg, Func<string, bool> condition = null)
        {
            string input;

            do
            {
                Console.Write(msg);
                input = Console.ReadLine().Trim();
            } while (!condition(input));

            return input;
        }

        public static int ReadIntegerFromConsole(string msg, Func<int, bool> condition = null)
        {
            int number;
            string input;

            do
            {
                Console.Write(msg);
                input = Console.ReadLine().Trim();
            } while (!int.TryParse(input, out number) && !condition(number));

            return number;
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
