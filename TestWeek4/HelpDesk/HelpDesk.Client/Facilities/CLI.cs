using HelpDesk.Core;
using HelpDesk.Core.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HelpDesk.Client.Facilities
{
    public class CLI
    {
        public MainBusinessLayer BusinessLayer { get; }
        private Dictionary<string, string> _commandInfo;
        private Dictionary<string, Action> _commandAction;

        public CLI (MainBusinessLayer businessLayer)
        {
            BusinessLayer = businessLayer;

            _commandInfo = new Dictionary<string, string>
            {
                { "LT", "List all tickets" },
                { "OT", "Open new ticket" },
                { "UT", "Update a ticket state" },
                { "RT", "Remove a ticket (permanently delete from storage)" },
                { "TBI", "Retrieve a ticket by Id" },
                { "TBS", "Retrieve all tickets in a given state" },
                { "H", "Show this menu" },
                { "Q", "Exit the program" }
            };

            _commandAction = new Dictionary<string, Action>
            {
                { "UT", UpdateTicketState },
                { "OT", OpenTicket },
                { "RT", RemoveTicket },
                { "LT", ListAllTickets },
                { "TBI", GetTicketById },
                { "TBS", GetTicketsByState },
                { "H", PrintMenu },
                { "Q", ExitProgram }
            };
        }

        public void PrintMenu()
        {
            Console.WriteLine("\nPlease choose one of the following options:");
            Console.WriteLine("===========================================");
            foreach (var command in _commandInfo)
                Console.WriteLine($"    |\t{command.Key} \t | {command.Value}");
            Console.WriteLine();
        }

        public void OpenTicket()
        {
            Console.WriteLine();

            // Automatically assign id
            int id = BusinessLayer.FetchAll().Max(t => t.Id) + 1;

            // Read assignee name
            string assignee = Utils.ReadFromConsole(
                "Please insert the assignee name: ",
                s => !String.IsNullOrEmpty(s) && !String.IsNullOrWhiteSpace(s) && s.All(c => !char.IsDigit(c)));

            // Read ticket category
            TicketCategory ticketCategory = 0;

            string categories = String.Join(", ", Enum.GetNames(typeof(TicketCategory)));

            // Read input from console (will be a valid input?)
            Utils.ReadFromConsole(
                $"Please specify a state [{categories}]: ",
                s => Enum.TryParse(s, out ticketCategory) &&
                    BusinessLayer.FetchAll().Any(x => x.Category == ticketCategory));

            // Read customer name
            string customer = Utils.ReadFromConsole(
                "Please insert the customer name: ",
                s => !String.IsNullOrEmpty(s) && !String.IsNullOrWhiteSpace(s) && s.All(c => !char.IsDigit(c)));

            // Read short description
            string shortDescription = Utils.ReadFromConsole(
                "Please insert a short description for the problem: ",
                s => !String.IsNullOrEmpty(s) && !String.IsNullOrWhiteSpace(s));

            // Read long description
            string longDescription = Utils.ReadFromConsole(
                "Please insert a long description for the problem: ",
                s => s.Length >= 0);

            // Instantiate new ticket
            Ticket ticket = new Ticket(id, assignee, ticketCategory, shortDescription, longDescription);

            // Save ticket in persistent storage
            BusinessLayer.OpenTicket(ticket);

            Console.WriteLine("Done!");
            Console.WriteLine();
        }

        public void UpdateTicketState()
        {
            Console.WriteLine();

            int id = 0;

            // Read input from console (will be a valid input?)
            Utils.ReadFromConsole(
                "Please provide a valid id [1..?]: ",
                s => int.TryParse(s, out id) && BusinessLayer.FetchAll().Any(x => x.Id == id));

            TicketState ticketState = 0;
            string states = String.Join(", ", Enum.GetNames(typeof(TicketState)));

            Utils.ReadFromConsole(
                $"Please specify a state [{states}]: ",
                s => Enum.TryParse(s, out ticketState) && BusinessLayer.FetchAll().Any(x => x.State == ticketState));

            if (ticketState == 0)
                throw new ArgumentException();

            BusinessLayer.Update(id, ticketState);

            Console.WriteLine("Done!");
            Console.WriteLine();
        }

        public void RemoveTicket()
        {
            Console.WriteLine();

            int id = 0;

            // Read input from console (will be a valid input?)
            Utils.ReadFromConsole(
                "Please provide a valid id [1..?]: ",
                s => int.TryParse(s, out id) && BusinessLayer.FetchAll().Any(x => x.Id == id));

            if (id == 0)
                throw new ArgumentException();

            // Perform requested removal
            Ticket toBeRemoved = BusinessLayer.FetchAll().FirstOrDefault(x => x.Id == id);
            BusinessLayer.RemoveTicket(toBeRemoved);

            Console.WriteLine("Removed!");
            Console.WriteLine();
        }

        public void ListAllTickets()
        {
            Console.WriteLine();

            foreach (Ticket ticket in BusinessLayer.FetchAll())
                Console.WriteLine(ticket.ToString());
        }

        public void GetTicketById()
        {
            Console.WriteLine();

            int id = 0;

            // Read input from console (will be a valid input?)
            Utils.ReadFromConsole(
                "Please provide a valid id [1..?]: ",
                s => int.TryParse(s, out id) && BusinessLayer.FetchAll().Any(x => x.Id == id));

            if (id == 0)
                throw new ArgumentException();

            // Select requested ticket
            Ticket ticket = BusinessLayer.GetById(id);

            // Print to console
            Console.WriteLine();
            Console.WriteLine(ticket.ToString());
        }

        public void GetTicketsByState()
        {
            Console.WriteLine();

            TicketState ticketState = 0;

            string states = String.Join(", ", Enum.GetNames(typeof(TicketState)));

            // Read input from console (will be a valid input?)
            string input = Utils.ReadFromConsole(
                $"Please specify a state [{states}]: ",
                s => Enum.TryParse(s, out ticketState) && BusinessLayer.FetchAll().Any(x => x.State == ticketState));

            if (ticketState == 0)
                throw new ArgumentException();

            // Select requested tickets
            List<Ticket> tickets = (List<Ticket>)BusinessLayer.GetTicketsByState(ticketState);

            // Print to console
            tickets.ForEach(x => Console.WriteLine(x.ToString()));
        }

        public void Run()
        {
            Console.WriteLine("=== WELCOME TO YOUR FRIENDLY HELPDESK SERVICE ===");

            PrintMenu();

            do
            {
                // Read input from console (will be a valid input?)
                string input = Utils.ReadFromConsole(
                    "Please choose a valid option: ",
                    s => _commandInfo.Keys.Contains(s.ToUpper()));

                // Match the _commandInfo key
                input = input.ToUpper();

                // Launch the command
                _commandAction[input]();
            } while (true);
        }

        public void ExitProgram()
        {
            Environment.Exit(0);
        }
    }
}
