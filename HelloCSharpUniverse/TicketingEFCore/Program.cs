using System;
using EasyConsoleFramework;
using TicketingEFCore.Catalogue;

namespace TicketingEFCore
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CLI cli = new CLI();
            cli.SetApplicationName("Ticketing on EFCore 5");

            cli.AddAction("L", "List all tickets", CRUD.ListTickets);
            cli.AddAction("I", "Insert new ticket", CRUD.InsertTicket);
            cli.AddAction("D", "Delete ticket", CRUD.DeleteTicket);

            cli.Run();
        }
    }
}