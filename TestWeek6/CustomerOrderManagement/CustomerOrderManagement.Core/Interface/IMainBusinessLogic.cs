using CustomerOrderManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerOrderManagement.Core.Interface
{
    public interface IMainBusinessLogic
    {
        /// <summary>
        /// Fetches customers, with or without filtering
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IList<Customer> FetchCustomers(Func<Customer, bool> filter = null);

        /// <summary>
        /// Gets single customer, given the ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Customer GetCustomer(int id);

        /// <summary>
        /// Adds a customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        bool AddCustomer(Customer customer);

        /// <summary>
        /// Updates a customer's data
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        bool UpdateCustomer(Customer customer);

        /// <summary>
        /// Removes a customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        bool RemoveCustomer(Customer customer);

        /// <summary>
        /// Removes a customer, given the ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool RemoveCustomerById(int id);

        /// <summary>
        /// Fetches orders, with or without filtering
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IList<Order> FetchOrders(Func<Order, bool> filter = null);

        /// <summary>
        /// Gets single order, given the ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Order GetOrder(int id);

        /// <summary>
        /// Add an order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        bool AddOrder(Order order);

        /// <summary>
        /// Updates an order's data
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        bool UpdateOrder(Order order);

        /// <summary>
        /// Removes an order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        bool RemoveOrder(Order order);

        /// <summary>
        /// Removes an order, given the ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool RemoveOrderById(int id);
    }
}