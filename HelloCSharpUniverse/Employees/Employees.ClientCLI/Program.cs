using EasyConsoleFramework;
using Employees.Core.BusinessLogic;
using Employees.Core;
using Employees.Core.Interface;
using Microsoft.Extensions.DependencyInjection;
using Employees.Core.EF;
using Employees.Core.EF.Repository;

namespace Employees.ClientCLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Register dependencies
            var serviceProvider = new ServiceCollection()
                .AddTransient<EmployeeContext>()
                .AddTransient<IEmployeeRepository, EmployeeRepositoryEF>()
                .AddTransient<IMainBusinessLogic, MainBusinessLogic>()
                .BuildServiceProvider();

            // This automatically calls the MainBusinessLogic constructor
            var logic = serviceProvider.GetService<IMainBusinessLogic>();

            CLI cli = new CLI();

            cli.SetApplicationName("EMPLOYEES");
            cli.AddAction("L", "List all employees", () => logic.GetAllEmployees());

            cli.Run();
        }
    }
}