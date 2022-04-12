using Employees.Core.BusinessLogic;
using Employees.Core.EF;
using Employees.Core.EF.Repository;
using Employees.Core.Entities;
using Employees.Core.Interface;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Employees.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class EmployeeService : IEmployeeService
    {
        private readonly IMainBusinessLogic _logic;

        public EmployeeService()
        {
            var serviceProvider = new ServiceCollection()
                .AddTransient<EmployeeContext>()
                .AddTransient<IEmployeeRepository, EmployeeRepositoryEF>()
                .AddTransient<IMainBusinessLogic, MainBusinessLogic>()
                .BuildServiceProvider();

            _logic = serviceProvider.GetService<IMainBusinessLogic>();
        }

        public bool AddNewEmployee(Employee employee)
        {
            return _logic.AddNewEmployee(employee);
        }

        public List<Employee> GetEmployees()
        {
            return _logic.GetAllEmployees();
        }
    }
}