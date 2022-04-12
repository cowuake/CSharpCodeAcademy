using System;

namespace Employees.Core.Entities
{
    public class Employee
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }
    }
}