using System;
using System.Collections.Generic;
using System.Text;

namespace EasyConsoleFramework.Utils
{
    public static class ErrorHandling
    {
        public static void Catch(Exception exception)
        {
            string type = exception.GetType().Name;
            string source = exception.Source.ToString();

            // Print exception type
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"{type}:");

            // Print exception text
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\t{exception.Message}");

            // Print exception source
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"\t(Source: {source})");

            Console.ResetColor();
            Console.WriteLine();

            if (exception.InnerException != null)
                Catch(exception.InnerException);
        }
    }
}