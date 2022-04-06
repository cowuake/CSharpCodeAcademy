using System;
using System.Threading.Tasks;

// NOTE: this project relies on the pizza_restaurant database created in TestWeek3!!

namespace PizzaRestaurant.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            bool connected = false;
            bool notConnected = true;

            // CONNECTED MODE EXAMPLES
            if (connected)
            {
                // Synchronous connection opening
                AdoNetExamples_connected.ConnectionExample();

                Console.WriteLine();

                // Asynchronous connection opening
                await AdoNetExamples_connected.ConnectionExampleAsync();

                Console.WriteLine();

                // Read data from database
                AdoNetExamples_connected.DataReaderExample();

                Console.WriteLine();

                // Read data from database passing a parameter
                AdoNetExamples_connected.DataReaderWithParamsExample(7.5m);

                Console.WriteLine();

                // Insert record in database table
                AdoNetExamples_connected.InsertExample();

                Console.WriteLine();

                // Insert record in database table using a parameter
                AdoNetExamples_connected.InsertWithParamsExample();

                Console.WriteLine();

                // Execute scalar command
                AdoNetExamples_connected.ExecuteScalarExample();

                Console.WriteLine();

                // Call stored procedure with parameters
                AdoNetExamples_connected.CallStoredProcedureWithParamsExample();

                Console.WriteLine();

                // Update a table in database
                AdoNetExamples_connected.UpdateExample();

                Console.WriteLine();

                // Update a table in database with params
                AdoNetExamples_connected.UpdateWithParamsExample("Ultimate Pizza");

                Console.WriteLine();

                // Delete record in database table
                AdoNetExamples_connected.DeleteExample();

                Console.WriteLine();

                // Delete record in database table with params
                AdoNetExamples_connected.DeleteWithParamsExample("Artichokes");

                Console.WriteLine();

                // Execute query returning more than one set from database
                AdoNetExamples_connected.MultipleResultSetsExample();

                Console.WriteLine();
            }

            // NOT CONNECTED MODE EXAMPLES
            if (notConnected)
            {
                AdoNetExamples_disconnected.ReadDataExample();

                Console.WriteLine();

                AdoNetExamples_disconnected.InsertExample();

                Console.WriteLine();

                AdoNetExamples_disconnected.UpdateExample();

                Console.WriteLine();

                AdoNetExamples_disconnected.DeleteExample();

                Console.WriteLine();

                // Not exactly in non-connected mode...
                AdoNetExamples_disconnected.TransactionExample();

                Console.WriteLine();
            }

            Console.Write("Press any key to exit... ");
            Console.ReadKey();
        }
    }
}