using CustomerOrderManagement.Core.Entities;
using CustomerOrderManagement.Core.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerOrderManagement.Core.BusinessLogic
{
    public class MainBusinessLogic : IMainBusinessLogic
    {
        private readonly ICustomerRepository _customers;
        private readonly IOrderRepository _orders;

        public MainBusinessLogic(ICustomerRepository customers, IOrderRepository orders)
        {
            _customers = customers;
            _orders = orders;
        }

        public bool AddCustomer(Customer customer)
            => _customers.Add(customer);

        public bool AddOrder(Order order)
            => _orders.Add(order);

        public IList<Customer> FetchCustomers(Func<Customer, bool> filter = null)
            => _customers.Fetch(filter) as IList<Customer>;

        public IList<Order> FetchOrders(Func<Order, bool> filter = null)
            => _orders.Fetch(filter) as IList<Order>;

        public Customer GetCustomer(int id)
            => _customers.Get(id);

        public Order GetOrder(int id)
            => _orders.Get(id);

        public bool RemoveCustomer(Customer customer)
            => _customers.Remove(customer);

        public bool RemoveCustomerById(int id)
        {
            Customer customer = _customers.Get(id);
            return _customers.Remove(customer);
        }

        public bool RemoveOrder(Order order)
            => _orders.Remove(order);

        public bool RemoveOrderById(int id)
        {
            Order order = _orders.Get(id);
            return _orders.Remove(order);
        }

        public bool UpdateCustomer(Customer customer)
            => _customers.Update(customer);

        public bool UpdateOrder(Order order)
            => _orders.Update(order);
    }
}