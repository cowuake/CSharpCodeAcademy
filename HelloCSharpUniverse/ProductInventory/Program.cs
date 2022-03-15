using System;
using System.Collections.Generic;
using System.Text;
using ProductInventory.Model;

namespace ProductInventory
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Product inventory ===");
            Console.WriteLine();

            IList<Product> products = new List<Product>()
            {
                new Product("asdf", "something", 3000.0m),
                new Product("fsfd", "something else", 2000.0m)
            };

            foreach (Product p in products)
            {
                Console.WriteLine($"Going to save product {p.Code} disk...");

                Console.WriteLine(p.SaveProduct() ? "Success." : "Failed");
            }

            Console.WriteLine();
            Console.Write("Press any key to exit ");
            Console.ReadKey();
        }
    }
}
