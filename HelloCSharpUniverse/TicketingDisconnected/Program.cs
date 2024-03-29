﻿using System;
using System.Data.SqlClient;
using System.IO;
using EasyConsoleFramework;
using Microsoft.Extensions.Configuration;

namespace TicketingDisconnected
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Read configuration
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Retrieve connection string from configuration
            string cs = config.GetConnectionString("ticketing");

            // Main loop
            if (CheckConnection(cs))
            {
                // Instantiate CLI object
                CLI cli = new CLI();

                cli.SetApplicationName("Ticketing in disconnected mode");

                MethodProvider provider = new MethodProvider(cs);

                cli.AddAction("L", "List all tickets", () => provider.ListAllTickets());
                cli.AddAction("I", "Insert ticket", () => provider.InsertTicket());
                cli.AddAction("D", "Delete ticket", () => provider.DeleteTicket());
                cli.AddAction("U", "Delete ticket", () => provider.UpdateDatabase());

                // Run the (user) command line interface
                cli.Run();
            }
        }

        internal static bool CheckConnection(string connectionString)
        {
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CONNECTION ERROR: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }

            return true;
        }
    }
}