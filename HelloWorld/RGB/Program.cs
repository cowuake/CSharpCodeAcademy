using System;
using MainLibrary;

namespace RGB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("============================");
            Console.WriteLine("=== RGB to HEX converter ===");
            Console.WriteLine("============================");

            byte red = InputLib.ReadByteFromConsole("Insert RED coordinate [0-255]: ");
            byte green = InputLib.ReadByteFromConsole("Insert GREEN coordinate [0-255]: ");
            byte blue = InputLib.ReadByteFromConsole("Insert GREEN coordinate [0-255]: ");

            Console.WriteLine();

            Console.WriteLine($"The color is: [{red}, {green}, {blue}]");
            Console.WriteLine($"In HEX: {red:X2}{green:X2}{blue:X2}");
        }
    }
}
