using System;

namespace Library
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
    }
}
