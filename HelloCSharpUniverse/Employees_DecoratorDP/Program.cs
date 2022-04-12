using System;
using Employees_DecoratorDP.Model;
using Employees_DecoratorDP.Model.Benefits;

namespace Employees_DecoratorDP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Demonstrating the Decorator design pattern ===");
            Console.WriteLine();

            Worker worker = new Employee("XX1", "John", "Carmack", DateTime.Now);
            Worker bp = new EmployeeWithBenefits(worker);

            CompanyCar car = new CompanyCar("Ferrari Testarossa", "id Software");
            HealthInsurance insurance = new HealthInsurance("ZZ1993");
            PaidParking parking = new PaidParking("LHAG88");
            RestaurantTickets ticket = new RestaurantTickets("EAT_0798X", 270);

            (bp as EmployeeWithBenefits).AddBenefit(car);
            (bp as EmployeeWithBenefits).AddBenefit(insurance);
            (bp as EmployeeWithBenefits).AddBenefit(parking);
            (bp as EmployeeWithBenefits).AddBenefit(ticket);

            bp.PrintDetails();

            Console.Write("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
