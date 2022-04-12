using System;
using System.Runtime.Serialization;

namespace Employees.Core.Entities
{
    [DataContract]
    public class Employee
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string FirstName { get; set; }
        
        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public decimal AnnualSalary { get; set; }
    }
}