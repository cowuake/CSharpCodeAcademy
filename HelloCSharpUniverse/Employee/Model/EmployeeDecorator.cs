using System;
using System.Collections.Generic;
using System.Text;

namespace Employees_DecoratorDP.Model
{
    // This is Decorator in the standard Decorator UML diagram
    public abstract class EmployeeDecorator : Worker
    {
        protected Worker _worker;

        public EmployeeDecorator(Worker worker)
        {
            _worker = worker;
        }

        // This is Operation in the standard Decorator UML diagram
        public override void PrintDetails()
        {
            // Functionalitis of Operation will be extended in ConcreteDecorator
            _worker.PrintDetails();
        }
    }
}