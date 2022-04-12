using System;
using System.Collections.Generic;
using Employees_DecoratorDP.Model.Benefits;

namespace Employees_DecoratorDP.Model
{
    // This is ConcreteDecorator in the standard Decorator UML diagram
    public class EmployeeWithBenefits : EmployeeDecorator
    {
        private IList<IBenefit> _benefits;

        public EmployeeWithBenefits(Worker worker) : base(worker)
        {
            _benefits = new List<IBenefit>();
        }

        public void AddBenefit(IBenefit benefit)
        {
            _benefits.Add(benefit);
        }

        // This is Operation in the standard Decorator UML diagram
        public override void PrintDetails()
        {
            base.PrintDetails();

            for(int i = 1; i <= _benefits.Count; i++)
            {
                Console.WriteLine($"Benefit {i}/{_benefits.Count}");
                Console.WriteLine(_benefits[i-1].GetPrintableDetails());
                Console.WriteLine();
            }
        }
    }
}
