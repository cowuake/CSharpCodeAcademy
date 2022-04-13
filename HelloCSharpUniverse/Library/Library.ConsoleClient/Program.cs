using EasyConsoleFramework;
//using Library.ConsoleClient.Proxy;
using System.ServiceModel;
using System;

namespace Library.ConsoleClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CLI cli = new CLI();

            //var client = new

            cli.SetApplicationName("LIBRARY CONSOLE CLIENT");

            cli.Run();
        }
    }
}