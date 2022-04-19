using EasyConsoleFramework;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace Library.WebAPI.ConsoleClient
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

                cli.AddAction("I", "Insert a book", () => client.InsertBook().Wait());
                cli.AddAction("L", "List all books", () => client.ListAllBooks().Wait());

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