using System;

namespace Employees_DecoratorDP.Model
{
    // This is ConcreteComponent in the standard Decorator UML diagram
    public class Employee : Worker
    {
        public string ID { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public DateTime DateOfRecruitment { get; }

        public Employee() { }

        public Employee(string id, string firstName, string lastName, DateTime dateOfRecruitment)
        {
            ID = id;
            FirstName = firstName;
            LastName = lastName;
            DateOfRecruitment = dateOfRecruitment;
        }

        // This is Operation in the standard Decorator UML diagram
        public override void PrintDetails()
        {
            Console.WriteLine($"Employee's ID: {ID}");
            Console.WriteLine($"Employee's name: {FirstName} {LastName}");
            Console.WriteLine($"Employee's date of recruitment: {DateOfRecruitment:d}");
            Console.WriteLine();
        }
    }
}