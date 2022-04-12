using Employees.WCF;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Employees.SelfHosting
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.ReadKey();
            using (var host = new ServiceHost(typeof(EmployeeService)))
            {
                host.Open();
                Console.ReadKey();
            }
        }
    }
}