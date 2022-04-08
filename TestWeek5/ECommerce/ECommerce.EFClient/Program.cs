using EasyConsoleFramework;
using ECommerce.Core.Interface;
using ECommerce.Core.EF;
using System;

namespace ECommerce.EFClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CLI cli = new CLI();

            ICatalogue catalogue = new EFCatalogue();

            cli.SetApplicationName("E-Commerce Helper");

            cli.AddAction("IP", "Insert product", catalogue.InsertProduct); // Done
            cli.AddAction("CC", "Change product category", catalogue.ChangeProductCategory); // Done
            cli.AddAction("CV", "Change product visibility", catalogue.ChangeProductVisibility); // Done
            cli.AddAction("CD", "Change product details", catalogue.ChangeProductDetails); // Done
            cli.AddAction("LP", "List products", catalogue.ListVisibleProducts); // Done
            cli.AddAction("LO", "List orders (date newer to older)", catalogue.ListOrdersByDescendingDate); // Partially done
            cli.AddAction("LS", "List total sales by category", catalogue.ListTotalSalesByCategory);
            cli.AddAction("F", "List products, applying multiple filters", catalogue.ListProductsFiltered);
            cli.AddAction("FP", "List products, filtered by price", catalogue.ListProductsFilteredByPrice); // Done
            cli.AddAction("FN", "List products, filtered by name", catalogue.ListProductsFilteredByName); // Done
            cli.AddAction("FC", "List products, filtered by category", catalogue.ListProductsFilteredByCategory); // Done

            cli.Run();
        }
    }
}
