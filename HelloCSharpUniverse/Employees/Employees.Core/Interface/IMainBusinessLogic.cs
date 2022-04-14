using Employees.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.Core.Interface
{
    public interface IMainBusinessLogic
    {
        List<Employee> GetAllEmployees();

        Employee GetEmployeeById(int id);

        bool AddNewEmployee(Employee employee);

        Employee SearchEmployee(string firstName, string lastName);

        bool UpdateEmployee(Employee employee);

        bool DeleteEmployeeById(int id);
    }
}
