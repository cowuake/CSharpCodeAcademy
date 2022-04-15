using CustomerOrderManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CustomerOrderManagement.Services.WCF
{
    [ServiceContract]
    public interface ICustomerService
    {
        [OperationContract]
        Customer GetCustomer(int id);

        [OperationContract]
        IList<Customer> FetchCustomers(Func<Customer, bool> filter);

        [OperationContract]
        bool AddCustomer(Customer customer);

        [OperationContract]
        bool UpdateCustomer(Customer customer);

        [OperationContract]
        bool RemoveCustomer(Customer customer);
    }
}