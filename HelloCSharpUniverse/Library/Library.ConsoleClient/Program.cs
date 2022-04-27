using EasyConsoleFramework;
//using Library.ConsoleClient.Proxy;
using System.ServiceModel;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace Library.ConsoleClient
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

                cli.SetApplicationName("LIBRARY CONSOLE CLIENT");

                cli.AddAction("BL", "List all books", () => client.ListAllBooks().Wait());

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