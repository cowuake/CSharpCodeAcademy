using Employees.Core.Entities;
using Employees.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Employees.Core.BusinessLogic
{
    public class MainBusinessLogic : IMainBusinessLogic
    {
        private readonly IEmployeeRepository _repository;

        public MainBusinessLogic(IEmployeeRepository repository)
        {
            _repository = repository; // Injected!
        }
        
        public bool AddNewEmployee(Employee employee)
        {
            return _repository.Add(employee);
        }

        public bool DeleteEmployeeById(Employee employee)
        {
            return _repository.DeleteById(employee.Id);
        }

        public bool DeleteEmployeeById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Employee> GetAllEmployees()
        {
            return _repository.Fetch().ToList();
        }

        public Employee GetEmployeeById(int id)
        {
            return _repository.GetById(id);
        }

        public Employee SearchEmployee(string firstName, string lastName)
        {
            return _repository
                .Fetch(e => e.FirstName == firstName && e.LastName == lastName)
                .FirstOrDefault();
        }

        public bool UpdateEmployee(Employee employee)
        {
            return _repository.Update(employee);
        }

        public bool UpdateEmployeeAnnualSalary(Employee employee, decimal annualSalary)
        {
            if (employee == null)
                return false;

            employee.AnnualSalary = annualSalary;
            return true;
        }
    }
}
