using EasyConsoleFramework;
using Employees.WebAPI.ConsoleClient.Methods;
using System;

namespace Employees.WebAPI.ConsoleClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CLI cli = new CLI();
            Client client = new Client();

            cli.AddAction("L", "List all employees", async () => await client.EmployeeList());

            cli.RunAsync();
        }
    }
}