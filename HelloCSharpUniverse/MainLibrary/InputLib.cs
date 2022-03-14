using System;

namespace MainLibrary
{
    public class InputLib
    {
        //static T ReadValueFromConsole<T>(string msg)
        //{
        //    T value;
        //    string input;

        //    do
        //    {
        //        Console.Write(msg);
        //        input = Console.ReadLine();
        //    } while (T.TryParse(input, out value) == false);

        //    return value;
        //}

        public static int ReadIntegerFromConsole(string msg, Func<int, bool> condition)
        {
            int num;
            string input;

            do
            {
                Console.Write(msg);
                input = Console.ReadLine();
            } while (!(int.TryParse(input, out num) && condition(num)));

            return num;
        }

        public static string ReadFromConsoleConditionally(string msg, Func<string, bool> condition)
        {
            string input;

            do
            {
                Console.Write(msg);
                input = Console.ReadLine();
            } while (!condition(input));

            return input;
        }

        public static byte ReadByteFromConsole(string msg)
        {
            byte b;
            string input;

            do
            {
                Console.Write(msg);
                input = Console.ReadLine();
            } while (!byte.TryParse(input, out b));

            return b;
        }

        public static double ReadDoubleFromConsole(string msg)
        {
            double d;
            string input;

            do
            {
                Console.Write(msg);
                input = Console.ReadLine();
            } while (!double.TryParse(input, out d));

            return d;
        }

        public static byte ReadByteFromConsole(string msg, Func<byte, bool> condition)
        {
            byte b;
            string input;

            do
            {
                Console.Write(msg);
                input = Console.ReadLine();
            } while (!(byte.TryParse(input, out b) && condition(b)));

            return b;
        }

        public static DateTime ReadDateTimeFromConsole(string msg)
        {
            DateTime dt;
            string input;

            do
            {
                Console.Write(msg);
                input = Console.ReadLine();
            } while (!DateTime.TryParse(input, out dt));

            return dt;
        }

        //public static T ReadValueFromConsole<T>(string msg)
        //{
        //    T value;
        //    string input;

        //    do
        //    {
        //        Console.Write(msg);
        //        input = Console.ReadLine();
        //    } while (!T.TryParse(input, out value));

        //    return value;
        //}
    }
}
