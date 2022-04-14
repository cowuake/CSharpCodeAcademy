using EasyConsoleFramework;
using System;

namespace Library.WebAPI.ConsoleClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CLI cli = new CLI();

            cli.Run();
        }
    }
}