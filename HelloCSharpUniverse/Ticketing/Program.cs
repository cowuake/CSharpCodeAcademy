using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data.Common;
using Ticketing.Client;

namespace Ticketing
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

            if (CheckConnection(cs))
            {
                // Instantiate CLI object
                CLI cli = new CLI(cs);

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