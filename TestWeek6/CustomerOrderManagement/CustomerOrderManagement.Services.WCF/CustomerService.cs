using CustomerOrderManagement.Core.BusinessLogic;
using CustomerOrderManagement.Core.EF.DataContext;
using CustomerOrderManagement.Core.EF.Repositories;
using CustomerOrderManagement.Core.Entities;
using CustomerOrderManagement.Core.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CustomerOrderManagement.Services.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class CustomerService : ICustomerService
    {
        private readonly IMainBusinessLogic _logic;

        public CustomerService()
        {
            var serviceProvider = new ServiceCollection()
                .AddTransient<CustomerOrderManagementContext>()
                .AddTransient<ICustomerRepository, EFCoreCustomerRepository>()
                .AddTransient<IOrderRepository, EFCoreOrderRepository>()
                .AddTransient<IMainBusinessLogic, MainBusinessLogic>()
                .BuildServiceProvider();

            _logic = serviceProvider.GetService<IMainBusinessLogic>();
        }

        public bool AddCustomer(Customer customer)
            => _logic.AddCustomer(customer);

        public IList<Customer> FetchCustomers(Func<Customer, bool> filter = null)
            => _logic.FetchCustomers(filter);

        public Customer GetCustomer(int id)
            => _logic.GetCustomer(id);

        public bool RemoveCustomer(Customer customer)
            => _logic.RemoveCustomer(customer);

        public bool RemoveCustomerById(int id)
            => _logic.RemoveCustomerById(id);

        public bool UpdateCustomer(Customer customer)
            => _logic.UpdateCustomer(customer);
    }
}