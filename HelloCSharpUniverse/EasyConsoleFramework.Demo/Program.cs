using System;
using EasyConsoleFramework;

namespace EasyConsoleFramework.Demo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CLI cli = new CLI();

            cli.SetApplicationName("EASY CONSOLE FRAMEWORK DEMO");

            cli.AddAction("PM", "Print an example of mocked data in tabular form", () => DemoCatalog.PrintMockedData());

            cli.Run();
        }
    }
}