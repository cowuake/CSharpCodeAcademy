using Employees.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.Core.Interface
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        // Not strictly required to add methods!
        // We are already including all from IRepository for the specific
        // entity class Employee (this is also an interface, after all)

        Employee GetByCode(string code);
    }
}