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

            Console.Write("Press any key to exit... ");
            Console.ReadKey();
        }
    }
}
