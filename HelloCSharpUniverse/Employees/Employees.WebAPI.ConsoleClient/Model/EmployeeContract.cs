using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.WebAPI.ConsoleClient.Model
{
    public class EmployeeContract
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal AnnualSalary { get; set; }
    }
}