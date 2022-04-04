using System;
using System.Threading.Tasks;

// NOTE: this project relies on the pizza_restaurant database created in TestWeek3!!

namespace PizzaRestaurant.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Synchronous connection opening
            AdoNetExamples.ConnectionExample();

            Console.WriteLine();

            // Asynchronous connection opening
            await AdoNetExamples.ConnectionExampleAsync();

            Console.WriteLine();

            // Read data from database
            AdoNetExamples.DataReaderExample();

            Console.WriteLine();

            // Read data from database passing a parameter
            AdoNetExamples.DataReaderWithParamsExample(7.5m);

            Console.WriteLine();

            // Insert record in database table
            AdoNetExamples.InsertExample();

            Console.WriteLine();

            // Insert record in database table using a parameter
            AdoNetExamples.InsertWithParamsExample();

            Console.WriteLine();

            // Execute scalar command
            AdoNetExamples.ExecuteScalarExample();

            Console.WriteLine();

            // Call stored procedure with parameters
            AdoNetExamples.CallStoredProcedureWithParamsExample();

            Console.WriteLine();

            // Update a table in database
            AdoNetExamples.UpdateExample();

            Console.WriteLine();

            // Update a table in database with params
            AdoNetExamples.UpdateWithParamsExample("Ultimate Pizza");

            Console.WriteLine();

            // Delete record in database table
            AdoNetExamples.DeleteExample();

            Console.WriteLine();

            // Delete record in database table with params
            AdoNetExamples.DeleteWithParamsExample("Artichokes");

            Console.WriteLine();

            // Execute query returning more than one set from database
            AdoNetExamples.MultipleResultSetsExample();

            Console.Write("Press any key to exit... ");
            Console.ReadKey();
        }
    }
}