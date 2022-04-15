using CustomerOrderManagement.Core.EF.DataContext;
using CustomerOrderManagement.Core.Entities;
using CustomerOrderManagement.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomerOrderManagement.Core.EF.Repositories
{
    public class EFCoreCustomerRepository : ICustomerRepository
    {
        private readonly CustomerOrderManagementContext _context;

        public EFCoreCustomerRepository(CustomerOrderManagementContext context)
        {
            _context = context;
        }

        public bool Add(Customer entity)
        {
            try
            {
                _context.Customers.Add(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Customer> Fetch(Func<Customer, bool> filter = null)
        {
            try
            {
                if (filter != null)
                    return _context.Customers.Where(filter).ToList();

                return _context.Customers.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Customer Get(object key)
        {
            if (key == null)
                return null;

            try
            {
                return _context.Customers.Find(key);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Remove(Customer entity)
        {
            try
            {
                _context.Customers.Remove(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Customer entity)
        {
            if (entity == null)
                return false;

            try
            {
                _context.Update(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}