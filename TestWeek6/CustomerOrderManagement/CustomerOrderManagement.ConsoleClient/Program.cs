using CustomerOrderManagement.Core.Entities;
using CustomerOrderManagement.ConsoleClient;
using EasyConsoleFramework;
using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerOrderManagement.ConsoleClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string settingsFile = Path.Combine(
                Directory.GetCurrentDirectory(),
                Constants.APPLICATION_SETTINGS_FILE);

            IConfigurationRoot config = ReadJsonConfiguration(settingsFile);

            var serviceProvider = RegisterDependencies(config);

            using (var client = serviceProvider.GetService<Client>())
            {
                CLI cli = new CLI();
                cli.SetApplicationName(Constants.APPLICATION_NAME);

                cli.AddAction("CI", "Insert new customer", client.InsertCustomer);
                cli.AddAction("CL", "List all customers", client.ListAllCustomers);
                cli.AddAction("CU", "Update customer (by ID)", client.UpdateCustomer);
                cli.AddAction("CR", "Remove customer (by ID)", client.RemoveCustomerById);
                cli.AddAction("OI", "Insert new order", () => client.InsertOrder().Wait());
                cli.AddAction("OL", "List all orders", () => client.ListAllOrders().Wait());
                cli.AddAction("OY", "Print report for orders by year", () => client.ReportOrdersByYear().Wait());
                cli.AddAction("OU", "Update an order", () => client.UpdateOrder().Wait());
                cli.AddAction("OR", "Remove order (by ID)", () => client.RemoveOrder().Wait());
                cli.AddAction("OLC", "List customer's order", () => client.ListOrdersForCustomer().Wait());
                cli.AddAction("OD", "Print order's details", () => client.PrintOrderDetails().Wait());

                cli.Run();
            }
        }

        static IServiceProvider RegisterDependencies(IConfigurationRoot config)
        {
            var serviceProvider = new ServiceCollection()
                .AddTransient<Client>()
                .AddSingleton<IConfigurationRoot>(config)
                .BuildServiceProvider();

            return serviceProvider;
        }

        static IConfigurationRoot ReadJsonConfiguration(string pathTofile)
        {
            if (!File.Exists(pathTofile))
                throw new FileNotFoundException();

            return new ConfigurationBuilder()
                .AddJsonFile(pathTofile)
                .Build();
        }
    }
}